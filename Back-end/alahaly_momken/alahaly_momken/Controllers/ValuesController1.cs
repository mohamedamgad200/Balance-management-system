using alahaly_momken.Entites;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Threading.Tasks;
using YourNamespace;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace alahaly_momken.Controllers
{
    public class CompanyUser : Company { }
    public class BankUser : Bank { }

    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController1 : ControllerBase
    {
        private static int? id_bank;
        private static int? id_company;
        private static int? updatedDepositId;
        private static int? updatedDepitId;
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public ValuesController1(ApplicationDbContext context, IWebHostEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
        }

        [HttpPost("CreateBank")]
        public async Task<ActionResult<Bank>> CreateBank([FromBody] BankCreationRequest request)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Bank bank = new Bank
                    {
                        name = request.Name,
                        email = request.Email,
                        password = request.Password,
                        Role = "Bank"
                    };
                    _context.banks.Add(bank);
                    await _context.SaveChangesAsync();
                    return Ok(bank);
                }
                else
                {
                    return BadRequest(ModelState);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error creating bank: {ex.Message}");
            }
        }

        [HttpPost("CreateCompany")]
        public async Task<ActionResult<Company>> CreateCompany([FromBody] CompanyCreationRequest request)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Company company = new Company
                    {
                        name = request.Name,
                        email = request.Email,
                        password = request.Password,
                        Role = "Company"
                    };
                    _context.companies.Add(company);
                    await _context.SaveChangesAsync();
                    return Ok(company);
                }
                else
                {
                    return BadRequest(ModelState);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error creating company: {ex.Message}");
            }
        }
        public CompanyUser companyUser = new CompanyUser();

        public BankUser bankUser = new BankUser();


        [HttpPost("Authenticate")]
        public async Task<ActionResult<string>> Authenticate([FromBody] LoginRequest request)
        {
            try
            {
                string role = await FindUserRole(request.Email, request.Password);
                if (!string.IsNullOrEmpty(role))
                {
                    if (role == "Company")
                    {
                        var company = await _context.companies.FirstOrDefaultAsync(u => u.email == request.Email && u.password == request.Password);
                        id_company = company.id;
                        companyUser.id = company.id;
                        companyUser.id = company.id; companyUser.name = company.name; companyUser.email = request.Email;
                        companyUser.password = request.Password;
                        companyUser.Role = role;
                        companyUser.user_depoists = company.user_depoists;

                    }
                    else if (role == "Bank")
                    {
                        var bank = await _context.banks.FirstOrDefaultAsync(u => u.email == request.Email && u.password == request.Password);
                        id_bank = bank.id;
                        bankUser.id = bank.id;
                        bankUser.name = bank.name;
                        bankUser.email = bank.email;
                        bankUser.password = bank.password;
                        bankUser.Role = bank.Role;
                        bankUser.user_depoists = bank.user_depoists;
                    }

                    return Ok(new { role });
                }
                else
                {
                    return Unauthorized(new { message = "Invalid email or password" });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error authenticating: {ex.Message}");
            }
        }



        [HttpPost("UploadFile")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UploadFile([FromForm] FileUploadRequest request)
        {
            try
            {
                if (request.File != null && request.File.Length > 0)
                {
                    var filePath = await WriteFile(request.File);

                    var companyId = id_company;

                    var bank = await _context.banks.FirstOrDefaultAsync(b => b.name == request.BankName);
                    if (bank == null)
                    {
                        return BadRequest(new { message = "Bank not found" });
                    }

                    var company = await _context.companies.FirstOrDefaultAsync(c => c.id == id_company);
                    if (company == null)
                    {
                        return BadRequest(new { message = "Company not found" });
                    }

                    // Store the current balance before the deposit
                    var balanceBefore = company.balance;

                    var deposit = new Depoist
                    {
                        Amount = request.Amount,
                        Status = "pending",
                        Date = DateTime.Now,
                        BankAccountNumber = request.BankAccountNumber,
                        ImagePath = $"https://localhost:7182/Upload/Files/{filePath}", // Save the image URL in the ImagePath property
                        BankName = request.BankName,
                        Companyid = (int)id_company,
                        Bankid = bank.id,
                        balancebefore = company.balance,
                        blanaceafter = company.balance
                        //+ (float)request.Amount
                        // Update balance after the deposit
                    };

                    _context.Depoists.Add(deposit);
                    await _context.SaveChangesAsync();

                    bank.user_depoists.Add(deposit);
                    company.user_depoists.Add(deposit);

                    // Update company's balance
                    // company.balance += (float)request.Amount;

                    await _context.SaveChangesAsync();

                    return Ok(new { message = "File uploaded successfully", imageUrl = deposit.ImagePath });
                }
                else
                {
                    return BadRequest(new { message = "No file uploaded" });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error uploading file: {ex.Message}");
            }
        }

        [HttpPost("CreateDepit")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UploadFileDebit([FromForm] FileUploadRequest request)
        {
            try
            {
                if (request.File != null && request.File.Length > 0)
                {
                    var filePath = await WriteFile(request.File);

                    var companyId = id_company;

                    var bank = await _context.banks.FirstOrDefaultAsync(b => b.name == request.BankName);
                    if (bank == null)
                    {
                        return BadRequest(new { message = "Bank not found" });
                    }

                    var company = await _context.companies.FirstOrDefaultAsync(c => c.id == id_company);
                    if (company == null)
                    {
                        return BadRequest(new { message = "Company not found" });
                    }

                    // Store the current balance before the deposit
                    var balanceBefore = company.balance;

                    var depit = new Depit
                    {
                        Amount = request.Amount,
                        Status = "pending",
                        Date = DateTime.Now,
                        BankAccountNumber = request.BankAccountNumber,
                        ImagePath = $"https://localhost:7182/Upload/Files/{filePath}",
                        BankName = request.BankName,
                        Companyid = (int)id_company,
                        Bankid = bank.id,
                        balancebefore = company.balance,
                        blanaceafter = company.balance
                        //+ (float)request.Amount
                        // Update balance after the deposit
                    };

                    _context.debts.Add(depit);
                    await _context.SaveChangesAsync();

                    bank.bank_depits.Add(depit);
                    company.company_depits.Add(depit);

                    // Update company's balance
                    //       company.balance += (float)request.Amount;

                    await _context.SaveChangesAsync();

                    return Ok(new { message = "File uploaded successfully" });
                }
                else
                {
                    return BadRequest(new { message = "No file uploaded" });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error uploading file: {ex.Message}");
            }
        }



        [HttpGet("GetBankDeposits")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetBankDeposits()
        {
            try
            {
                // Retrieve the bank based on the stored id_bank
                var bank = await _context.banks.Include(b => b.user_depoists).FirstOrDefaultAsync(b => b.id == id_bank);

                if (bank == null)
                {
                   return NotFound(new { message = "Bank not found" });
                }

                return Ok(bank.user_depoists);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error retrieving bank deposits: {ex.Message}");
            }
        }

        [HttpGet("GetBankDepits")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetBankDepits()
        {
            try
            {
                // Retrieve the bank based on the stored id_bank
                var bank = await _context.banks.Include(b => b.bank_depits).FirstOrDefaultAsync(b => b.id == id_bank);

                if (bank == null)
                {
                    return NotFound(new { message = "Bank not found" });
                }

                return Ok(bank.bank_depits);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error retrieving bank deposits: {ex.Message}");
            }
        }
        private int GetUpdatedDepositIdFromContext()
        {

            return updatedDepositId ?? throw new InvalidOperationException("Updated deposit ID is not available.");
        }
        private int GetUpdatedDepitIdFromContext()
        {

            return updatedDepitId ?? throw new InvalidOperationException("Updated depit ID is not available.");
        }

        [HttpPost("UpdateDepositStatus")]
        public async Task<IActionResult> UpdateDepositStatus(int depositId, string newStatus)
        {
            try
            {
                // Find the deposit by its ID
                var deposit = await _context.Depoists.FirstOrDefaultAsync(d => d.Id == depositId);
                updatedDepositId = depositId;
                if (deposit == null)
                {
                    return NotFound(new { message = "Deposit not found" });
                }

                // If the new status is "rejected", show the text input API
                if (newStatus == "rejected")
                {
                    // Retrieve the associated company using the Companyid of the deposit
                    var company = await _context.companies.FirstOrDefaultAsync(c => c.id == deposit.Companyid);
                    if (company == null)
                    {
                        return NotFound(new { message = "Company not found" });
                    }

                    // Store the current balance before and after rejection
                    deposit.balancebefore = company.balance;
                    deposit.blanaceafter = company.balance;

                    // Update the status of the deposit
                    deposit.Status = newStatus;

                    // Save changes to the database
                    await _context.SaveChangesAsync();

                    // Return a response indicating that the user needs to enter a note
                    return Ok(new { message = "Please provide a note for the rejection." });
                }

                // Update the status of the deposit
                deposit.Status = newStatus;

                // If the status is "success", update balancebefore and blanaceafter
                if (newStatus == "success")
                {
                    // Retrieve the associated company using the Companyid of the deposit
                    var company = await _context.companies.FirstOrDefaultAsync(c => c.id == deposit.Companyid);
                    var banke = await _context.banks.FirstOrDefaultAsync(b => b.id == id_bank);
                    if (company == null)
                    {
                        return NotFound(new { message = "Company not found" });
                    }

                    // Store the current balance before updating it
                    deposit.balancebefore = company.balance;
                    banke.balance += (float)deposit.Amount;
                    // Increase the company's balance by the amount of the deposit
                    company.balance += (float)deposit.Amount;

                    // Store the new balance after the deposit
                    deposit.blanaceafter = company.balance;

                }

                // Save changes to the database
                await _context.SaveChangesAsync();

                // Update the status in the company's deposit list
                var companyToUpdate = await _context.companies.FindAsync(deposit.Companyid);
                if (companyToUpdate != null)
                {
                    var depositToUpdateInCompany = companyToUpdate.user_depoists.FirstOrDefault(d => d.Id == depositId);
                    if (depositToUpdateInCompany != null)
                    {
                        depositToUpdateInCompany.Status = newStatus;
                    }
                }

                // Update the status in the bank's deposit list
                var bank = await _context.banks.FirstOrDefaultAsync();
                if (bank != null)
                {
                    var depositToUpdateInBank = bank.user_depoists.FirstOrDefault(d => d.Id == depositId);
                    if (depositToUpdateInBank != null)
                    {
                        depositToUpdateInBank.Status = newStatus;
                    }
                }

                // Save changes to the database
                await _context.SaveChangesAsync();

                return Ok(new { message = "Deposit status updated successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error updating deposit status: {ex.Message}");
            }
        }
        [HttpPost("UpdateDepitStatus")]
        public async Task<IActionResult> UpdateDepitStatus(int depitId, string newStatus)
        {
            try
            {
                // Find the deposit by its ID
                var depit = await _context.debts.FirstOrDefaultAsync(d => d.Id == depitId);
                updatedDepitId = depitId;
                if (depit == null)
                {
                    return NotFound(new { message = "Depit not found" });
                }

                // If the new status is "rejected", show the text input API
                if (newStatus == "rejected")
                {
                    // Retrieve the associated company using the Companyid of the deposit
                    var company = await _context.companies.FirstOrDefaultAsync(c => c.id == depit.Companyid);
                    if (company == null)
                    {
                        return NotFound(new { message = "Company not found" });
                    }

                    // Store the current balance before and after rejection
                    depit.balancebefore = company.balance;
                    depit.blanaceafter = company.balance;

                    // Update the status of the deposit
                    depit.Status = newStatus;

                    // Save changes to the database
                    await _context.SaveChangesAsync();

                    // Return a response indicating that the user needs to enter a note
                    return Ok(new { message = "Please provide a note for the rejection." });
                }

                // Update the status of the deposit
                depit.Status = newStatus;

                // If the status is "success", update balancebefore and blanaceafter
                if (newStatus == "success")
                {
                    // Retrieve the associated company using the Companyid of the deposit
                    var company = await _context.companies.FirstOrDefaultAsync(c => c.id == depit.Companyid);
                    var banke = await _context.banks.FirstOrDefaultAsync(b => b.id == id_bank);
                    if (company == null)
                    {
                        return NotFound(new { message = "Company not found" });
                    }

                    // Store the current balance before updating it
                    depit.balancebefore = company.balance;
                    banke.balance -= (float)depit.Amount;
                    // Increase the company's balance by the amount of the deposit
                    company.balance -= (float)depit.Amount;

                    // Store the new balance after the deposit
                    depit.blanaceafter = company.balance;

                }

                // Save changes to the database
                await _context.SaveChangesAsync();

                // Update the status in the company's deposit list
                var companyToUpdate = await _context.companies.FindAsync(depit.Companyid);
                if (companyToUpdate != null)
                {
                    var depositToUpdateInCompany = companyToUpdate.company_depits.FirstOrDefault(d => d.Id == depitId);
                    if (depositToUpdateInCompany != null)
                    {
                        depositToUpdateInCompany.Status = newStatus;
                    }
                }

                // Update the status in the bank's deposit list
                var bank = await _context.banks.FirstOrDefaultAsync();
                if (bank != null)
                {
                    var depositToUpdateInBank = bank.bank_depits.FirstOrDefault(d => d.Id == depitId);
                    if (depositToUpdateInBank != null)
                    {
                        depositToUpdateInBank.Status = newStatus;
                    }
                }

                // Save changes to the database
                await _context.SaveChangesAsync();

                return Ok(new { message = "Depit status updated successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error updating depit status: {ex.Message}");
            }
        }

        [HttpPost("AddNoteToRejectedDeposit")]
        public async Task<IActionResult> AddNoteToRejectedDeposit(string note)
        {
            try
            {
                // Get the updated deposit ID from the context
                int depositId = (int)updatedDepositId;

                // Find the deposit by its ID
                var deposit = await _context.Depoists.FirstOrDefaultAsync(d => d.Id == depositId);
                if (deposit == null)
                {
                    return NotFound(new { message = "Deposit not found" });
                }

                // Check if the status of the deposit is "rejected"
                if (deposit.Status != "rejected")
                {
                    return BadRequest(new { message = "This deposit is not rejected." });
                }

                // Update the note in the deposit
                deposit.Note = note;

                // Save changes to the database
                await _context.SaveChangesAsync();

                return Ok(new { message = "Note added to the rejected deposit successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error adding note to rejected deposit: {ex.Message}");
            }
        }
        [HttpPost("AddNoteToRejectedDepit")]
        public async Task<IActionResult> AddNoteToRejectedDepit(string note)
        {
            try
            {
                // Get the updated deposit ID from the context
                int depitId = (int)updatedDepitId;

                // Find the deposit by its ID
                var depit = await _context.debts.FirstOrDefaultAsync(d => d.Id == depitId);
                if (depit == null)
                {
                    return NotFound(new { message = "Depit not found" });
                }

                // Check if the status of the deposit is "rejected"
                if (depit.Status != "rejected")
                {
                    return BadRequest(new { message = "This depit is not rejected." });
                }

                // Update the note in the deposit
                depit.Note = note;

                // Save changes to the database
                await _context.SaveChangesAsync();

                return Ok(new { message = "Note added to the rejected depit successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error adding note to rejected deposit: {ex.Message}");
            }
        }



        [HttpGet("GetCompanyDeposits")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetCompanyDeposits()
        {
            try
            {
                // Retrieve the company based on the stored id_company
                var company = await _context.companies.Include(c => c.user_depoists).FirstOrDefaultAsync(c => c.id == id_company);

                if (company == null)
                {
                    return NotFound(new { message = "Company not found" });
                }

                return Ok(company.user_depoists);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error retrieving company deposits: {ex.Message}");
            }
        }
        [HttpGet("GetCompanyDepositsForDay")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetCompanyDepositsForDay([FromQuery] DateTime date)
        {
            try
            {
                // Define the start and end of the specified day
                var startDate = date.Date;
                var endDate = startDate.AddDays(1).AddTicks(-1); // Up to 11:59:59.999 PM of the same day

                // Retrieve the company based on the stored id_company
                var company = await _context.companies
                                            .Include(c => c.user_depoists)
                                            .FirstOrDefaultAsync(c => c.id == id_company);

                if (company == null)
                {
                    return NotFound(new { message = "Company not found" });
                }

                // Filter deposits within the specified date range
                var depositsInRange = company.user_depoists
                                             .Where(d => d.Date >= startDate && d.Date <= endDate&& d.Status == "success")
                                             .OrderBy(d => d.Date);

                // Calculate the count and total amount of deposits
                var totalCount = depositsInRange.Count();
                var totalAmount = depositsInRange.Sum(d => d.Amount);

                // Retrieve the opening balance (balance before the first deposit of the day)
                var openingBalance = depositsInRange.FirstOrDefault()?.balancebefore ?? 0;

                // Retrieve the closing balance (balance after the last deposit of the day)
                var closingBalance = depositsInRange.LastOrDefault()?.blanaceafter ?? 0;

                // Retrieve the date of the last deposit
                var lastDepositDate = depositsInRange.Any() ? depositsInRange.Max(d => d.Date) : DateTime.MinValue;

                var result = new
                {
                    OpeningBalance = openingBalance,
                    ClosingBalance = closingBalance,
                    TotalCount = totalCount,
                    TotalAmount = totalAmount,
                    LastDepositDate = lastDepositDate
                };

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error retrieving company deposits: {ex.Message}");
            }
        }



[HttpGet("GetCompanyDepitsForDay")]
[ProducesResponseType(StatusCodes.Status200OK)]
[ProducesResponseType(StatusCodes.Status404NotFound)]
public async Task<IActionResult> GetCompanyDepitsForDay([FromQuery] DateTime date)
{
    try
    {
        // Define the start and end of the specified day
        var startDate = date.Date;
        var endDate = startDate.AddDays(1).AddTicks(-1); // Up to 11:59:59.999 PM of the same day

        // Retrieve the company based on the stored id_company
        var company = await _context.companies
                                    .Include(c => c.company_depits)
                                    .FirstOrDefaultAsync(c => c.id == id_company);

        if (company == null)
        {
            return NotFound(new { message = "Company not found" });
        }

        // Filter depits within the specified date range and with success status
        var depitsInRange = company.company_depits
                                 .Where(d => d.Date >= startDate && d.Date <= endDate && d.Status == "success");

        // Calculate the count and total amount of depits
        var count = depitsInRange.Count();
        var totalAmount = depitsInRange.Sum(d => d.Amount);
        var lastDepositDate = depitsInRange.Any() ? depitsInRange.Max(d => d.Date) : DateTime.MinValue;
        var result = new
        {
            Count = count,
            TotalAmount = totalAmount,
            LastDepositDate = lastDepositDate
        };

        return Ok(result);
    }
    catch (Exception ex)
    {
        return StatusCode(StatusCodes.Status500InternalServerError, $"Error retrieving company depits: {ex.Message}");
    }
}
        [HttpGet("GetCompanyDepits")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetCompanyDepits()
        {
            try
            {
                // Retrieve the company based on the stored id_company
                var company = await _context.companies.Include(c => c.company_depits).FirstOrDefaultAsync(c => c.id == id_company);

                if (company == null)
                {
                    return NotFound(new { message = "Company not found" });
                }

                return Ok(company.company_depits);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error retrieving company deposits: {ex.Message}");
            }
        }

        [HttpGet("GetCompanyBalance")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetCompanyBalance()
        {
            try
            {
                // Retrieve the company based on the stored id_company
                var company = await _context.companies.FirstOrDefaultAsync(c => c.id == id_company);

                if (company == null)
                {
                    return NotFound(new { message = "Company not found" });
                }

                return Ok(new { balance = company.balance });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error retrieving company balance: {ex.Message}");
            }
        }


        [HttpGet("GetBankBalance")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetBankBalance()
        {
            try
            {
                // Retrieve the company based on the stored id_company
                var bank = await _context.banks.FirstOrDefaultAsync(c => c.id == id_bank);

                if (bank == null)
                {
                    return NotFound(new { message = "bank not found" });
                }

                return Ok(new { balance = bank.balance });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error retrieving company balance: {ex.Message}");
            }
        }


        [HttpPost("CorrectBalance")]
        public async Task<IActionResult> CorrectBalance([FromBody] CorrectionRequest request)
        {
            try
            {
                // Retrieve the bank based on the stored id_bank
                var bank = await _context.banks.FirstOrDefaultAsync(b => b.id == id_bank);

                if (bank == null)
                {
                    return NotFound(new { message = "Bank not found" });
                }

                // Store the current balance before the correction
                float balanceBefore = bank.balance;

                // Update the balance based on the type of correction
                if (request.Type == "increase")
                {
                    bank.balance += request.Amount;
                }
                else if (request.Type == "decrease")
                {
                    bank.balance -= request.Amount;
                }
                else
                {
                    return BadRequest(new { message = "Invalid correction type. Please use 'increase' or 'decrease'." });
                }

                // Store the new balance after the correction
                float balanceAfter = bank.balance;

                // Create a new Correction entry
                var correction = new Correction
                {
                    type = request.Type,
                    amount = request.Amount,
                    Bankid = bank.id,
                    balancebefore = balanceBefore,
                    blanaceafter = balanceAfter,
                    Date = DateTime.Now,
                };

                // Add the correction to the bank's corrections list and save changes
                bank.corrections.Add(correction);
                await _context.SaveChangesAsync();

                return Ok(new { message="Balance corrected successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error correcting balance: {ex.Message}");
            }
        }
        [HttpGet("GetAllCorrectionsForDay")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAllCorrectionsForDay([FromQuery] DateTime date)
        {
            try
            {
                // Define the start and end of the specified day
                var startDate = date.Date;
                var endDate = startDate.AddDays(1).AddTicks(-1); // Up to 11:59:59.999 PM of the same day

                // Retrieve all corrections within the specified date range
                var correctionsInRange = await _context.corrections
                                                        .Where(c => c.Date >= startDate && c.Date <= endDate)
                                                        .ToListAsync();

                if (correctionsInRange == null || !correctionsInRange.Any())
                {
                    return NotFound(new { message = "No corrections found for the specified day" });
                }

                // Filter corrections by type and calculate total amount for each type
                var increaseCorrections = correctionsInRange.Where(c => c.type == "increase").ToList();
                var decreaseCorrections = correctionsInRange.Where(c => c.type == "decrease").ToList();

                var increaseTotalAmount = increaseCorrections.Sum(c => c.amount);
                var decreaseTotalAmount = decreaseCorrections.Sum(c => c.amount);

                var result = new
                {
                    IncreaseCount = increaseCorrections.Count,
                    IncreaseTotalAmount = increaseTotalAmount,
                    DecreaseCount = decreaseCorrections.Count,
                    DecreaseTotalAmount = decreaseTotalAmount
                };

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error retrieving corrections: {ex.Message}");
            }
        }
        [HttpGet("enddayreport")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> EndDayReport([FromQuery] DateTime date)
        {
            try
            {
                // Define the start and end of the specified day
                var startDate = date.Date;
                var endDate = startDate.AddDays(1).AddTicks(-1); // Up to 11:59:59.999 PM of the same day

                // Retrieve the company based on the stored id_company
                var company = await _context.companies
                    .Include(c => c.user_depoists)
                    .Include(c => c.company_depits)
                    .FirstOrDefaultAsync(); // Modify this query as needed

                if (company == null)
                {
                    return NotFound(new { message = "Company not found" });
                }

                // Filter deposits within the specified date range
                var depositsInRange = company.user_depoists
                    .Where(d => d.Date >= startDate && d.Date <= endDate && d.Status == "success")
                    .OrderBy(d => d.Date);

                // Filter depits within the specified date range and with success status
                var depitsInRange = company.company_depits
                    .Where(d => d.Date >= startDate && d.Date <= endDate && d.Status == "success");

                // Filter corrections within the specified date range
                var correctionsInRange = await _context.corrections
                    .Where(c => c.Date >= startDate && c.Date <= endDate)
                    .ToListAsync();

                // Calculate the count and total amount of deposits
                var depositCount = depositsInRange.Count();
                var depositTotalAmount = depositsInRange.Sum(d => d.Amount);

                // Calculate the count and total amount of depits
                var depitCount = depitsInRange.Count();
                var depitTotalAmount = depitsInRange.Sum(d => d.Amount);

                // Calculate the total amount for each type of correction as floats
                var increaseCorrections = correctionsInRange.Where(c => c.type == "increase").ToList();
                var decreaseCorrections = correctionsInRange.Where(c => c.type == "decrease").ToList();

                var increaseTotalAmount = (float)increaseCorrections.Sum(c => c.amount);
                var decreaseTotalAmount = (float)decreaseCorrections.Sum(c => c.amount);

                // Retrieve the opening balance (balance before the first deposit of the day)
                var openingBalance = depositsInRange.FirstOrDefault()?.balancebefore ?? 0f;

                // Retrieve the closing balance (balance after the last deposit of the day)
                var lastDepositDate = depositsInRange.Any() ? depositsInRange.Max(d => d.Date) : DateTime.MinValue;
                var lastDebitDate = depitsInRange.Any() ? depitsInRange.Max(d => d.Date) : DateTime.MinValue;

                var closingBalance = 0f;
                if (lastDepositDate > lastDebitDate)
                {
                    closingBalance = depositsInRange.LastOrDefault()?.blanaceafter ?? 0f;
                }
                else
                {
                    closingBalance = depitsInRange.LastOrDefault()?.blanaceafter ?? 0f;
                }

                // Retrieve the date of the last deposit
                var lastTransactionDate = depitsInRange.Any() ? depitsInRange.Max(d => d.Date) :
                                       depositsInRange.Any() ? depositsInRange.Max(d => d.Date) :
                                       DateTime.MaxValue;

                // Calculate the balance after the last transaction
                var balanceAfterLastTransaction = (lastTransactionDate > startDate && lastTransactionDate <= endDate) ?
                                                       closingBalance : 0f;

                var result = new
                {
                    OpeningBalance = openingBalance,
                    ClosingBalance = closingBalance,
                    DepositCount = depositCount,
                    DepositTotalAmount = depositTotalAmount,
                    DepitCount = depitCount,
                    DepitTotalAmount = depitTotalAmount,
                    IncreaseCount = increaseCorrections.Count,
                    IncreaseTotalAmount = increaseTotalAmount,
                    DecreaseCount = decreaseCorrections.Count,
                    DecreaseTotalAmount = decreaseTotalAmount,
                    LastTransactionDate = lastTransactionDate,
                    BalanceAfterLastTransaction = balanceAfterLastTransaction
                };

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error retrieving end day report: {ex.Message}");
            }
        }






        private async Task<string> WriteFile(IFormFile file)
        {
            string filename = "";
            try
            {
                var extension = "." + file.FileName.Split('.')[file.FileName.Split('.').Length - 1];
                filename = DateTime.Now.Ticks.ToString() + extension;

                var filepath = Path.Combine(Directory.GetCurrentDirectory(), "Upload\\Files");

                if (!Directory.Exists(filepath))
                {
                    Directory.CreateDirectory(filepath);
                }

                var exactpath = Path.Combine(Directory.GetCurrentDirectory(), "Upload\\Files", filename);
                using (var stream = new FileStream(exactpath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return filename;
        }

        private async Task<string> FindUserRole(string email, string password)
        {
            var superAdmin = await _context.superAdmins.FirstOrDefaultAsync(u => u.email == email && u.password == password);
            if (superAdmin != null)
            {
                return superAdmin.Role;
            }

            var company = await _context.companies.FirstOrDefaultAsync(u => u.email == email && u.password == password);
            if (company != null)
            {
                return company.Role;
            }

            var bank = await _context.banks.FirstOrDefaultAsync(u => u.email == email && u.password == password);
            if (bank != null)
            {
                return bank.Role;
            }

            return null; // No match found
        }
    }

    public class BankCreationRequest
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class CompanyCreationRequest
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class LoginRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class FileUploadRequest
    {
        public IFormFile File { get; set; }
        public decimal Amount { get; set; }
        public string BankAccountNumber { get; set; }
        public String BankName { get; set; }

    }
    public class CorrectionRequest
    {
        public string Type { get; set; }
        public int Amount { get; set; }
    }
}























//[HttpPost("UpdateDepositStatus")]
//public async Task<IActionResult> UpdateDepositStatus(int depositId, string newStatus)
//{
//    try
//    {
//        // Find the deposit by its ID
//        var deposit = await _context.Depoists.FirstOrDefaultAsync(d => d.Id == depositId);
//        if (deposit == null)
//        {
//            return NotFound("Deposit not found");
//        }

//        // If the new status is "rejected", show the text input API
//        if (newStatus == "rejected")
//        {
//            // Retrieve the associated company using the Companyid of the deposit
//            var company = await _context.companies.FirstOrDefaultAsync(c => c.id == deposit.Companyid);
//            if (company == null)
//            {
//                return NotFound("Company not found");
//            }

//            // Store the current balance before and after rejection
//            deposit.balancebefore = company.balance;
//            deposit.blanaceafter = company.balance;

//            // Update the status of the deposit
//            deposit.Status = newStatus;

//            // Save changes to the database
//            await _context.SaveChangesAsync();

//            // Return a response indicating that the user needs to enter a note
//            return Ok("Please provide a note for the rejection.");
//        }

//        // Update the status of the deposit
//        deposit.Status = newStatus;

//        // If the status is "success", update balancebefore and blanaceafter
//        if (newStatus == "success")
//        {
//            // Retrieve the associated company using the Companyid of the deposit
//            var company = await _context.companies.FirstOrDefaultAsync(c => c.id == deposit.Companyid);
//            if (company == null)
//            {
//                return NotFound("Company not found");
//            }

//            // Store the current balance before updating it
//            deposit.balancebefore = company.balance;

//            // Increase the company's balance by the amount of the deposit
//            company.balance += (float)deposit.Amount;

//            // Store the new balance after the deposit
//            deposit.blanaceafter = company.balance;
//        }

//        // Save changes to the database
//        await _context.SaveChangesAsync();

//        // Update the status in the company's deposit list
//        var companyToUpdate = await _context.companies.FindAsync(deposit.Companyid);
//        if (companyToUpdate != null)
//        {
//            var depositToUpdateInCompany = companyToUpdate.user_depoists.FirstOrDefault(d => d.Id == depositId);
//            if (depositToUpdateInCompany != null)
//            {
//                depositToUpdateInCompany.Status = newStatus;
//            }
//        }

//        // Update the status in the bank's deposit list
//        var bank = await _context.banks.FirstOrDefaultAsync();
//        if (bank != null)
//        {
//            var depositToUpdateInBank = bank.user_depoists.FirstOrDefault(d => d.Id == depositId);
//            if (depositToUpdateInBank != null)
//            {
//                depositToUpdateInBank.Status = newStatus;
//            }
//        }

//        // Save changes to the database
//        await _context.SaveChangesAsync();

//        return Ok($"Deposit status updated successfully. New status: {newStatus}");
//    }
//    catch (Exception ex)
//    {
//        return StatusCode(StatusCodes.Status500InternalServerError, $"Error updating deposit status: {ex.Message}");
//    }
//}

//[HttpPost("AddNoteToRejectedDeposit")]
//public async Task<IActionResult> AddNoteToRejectedDeposit(int depositId, string note)
//{
//    try
//    {
//        // Find the deposit by its ID
//        var deposit = await _context.Depoists.FirstOrDefaultAsync(d => d.Id == depositId);
//        if (deposit == null)
//        {
//            return NotFound("Deposit not found");
//        }

//        // Check if the status of the deposit is "rejected"
//        if (deposit.Status != "rejected")
//        {
//            return BadRequest("This deposit is not rejected.");
//        }

//        // Update the note in the deposit
//        deposit.Note = note;

//        // Save changes to the database
//        await _context.SaveChangesAsync();

//        return Ok("Note added to the rejected deposit successfully.");
//    }
//    catch (Exception ex)
//    {
//        return StatusCode(StatusCodes.Status500InternalServerError, $"Error adding note to rejected deposit: {ex.Message}");
//    }
//}

