[System.Serializable]
public class RatingTier
{
    public float minimumRating;
    public string[] articleTitles;
    public string[] articleTexts;

    public string GetRandomArticle()
    {
        return articleTitles.GetRandomString();
    }

    public string GetRandomText()
    {
        return articleTexts.GetRandomString();
    }

    public bool TestForRating(decimal rating)
    {
        return (float)rating >= minimumRating;
    }
}
