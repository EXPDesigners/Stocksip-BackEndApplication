using System.ComponentModel.DataAnnotations.Schema;
using EntityFrameworkCore.CreatedUpdatedDate.Contracts;

namespace StockSip.Platform.API.IdentityAndAccessManagement.Domain.Model.Aggregates;

/// <summary>
/// This class represents the audit information for a user entity.
/// </summary>
public partial class User : IEntityWithCreatedUpdatedDate
{
    /// <summary>
    /// This is the date and time when the user was created.
    /// </summary>
    [Column("CreatedAt")] public DateTimeOffset? CreatedDate { get; set; }
    
    /// <summary>
    /// This is the date and time when the user information was last updated.
    /// </summary>
    [Column("UpdatedAt")] public DateTimeOffset? UpdatedDate { get; set; }
}