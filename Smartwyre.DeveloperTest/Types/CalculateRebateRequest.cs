namespace Smartwyre.DeveloperTest.Types;

public class CalculateRebateRequest
{
    public string RebateIdentifier { get; set; }

    public string ProductIdentifier { get; set; }

    public decimal Volume { get; set; }

    public IncentiveType IncentiveType { get; set; } // Getting incentive type as user input because database is not developed yet
}
