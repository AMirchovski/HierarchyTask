namespace HierarchyRefactored.Assets;

public class Mail
{
    public string MailText { get; private set; }
    public int FromId { get; private set; }
    public int ToId { get; private set; }

    public Mail(string mailText, int fromId, int toId)
    {
        MailText = mailText;
        FromId = fromId;
        ToId = toId;
    }

    public override string ToString()
    {
        return $"Mail sent from employee with id: {FromId} to supervisor with id: {ToId}\nText: {MailText}";
    }
}