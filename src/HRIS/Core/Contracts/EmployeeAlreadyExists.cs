namespace Nova.HRIS.Contracts;

public record EmployeeAlreadyExists
(
    string FirstName, 
    string MiddleName, 
    string LastName, 
    string NameSuffix, 
    bool? Sex, 
    DateTime? BirthDate
) : FailedResponse;