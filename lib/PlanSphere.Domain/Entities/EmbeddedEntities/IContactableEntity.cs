namespace Domain.Entities.EmbeddedEntities;

public interface IContactableEntity
{
    public string ContactName { get; set; }
    public string ContactEmail { get; set; }
    public string ContactPhoneNumber { get; set; }
}