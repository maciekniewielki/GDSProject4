using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Linq;

public class NewspaperViewer : MonoBehaviour
{
    public Image articleImage;
    public Text articleTitle;
    public Text articleText;

    public RatingTier[] ratingTiers;
    public Sprite[] images;

    void Start()
    {
        decimal rating = CalculationsManager.CalculatePlayerRating(GameObject.Find("MatchStats").GetComponent<StatisticsManager>().endStatistics);
        RatingTier rt = GetTierByRating(rating);
        ShowNewspaper(rt);
    }

    RatingTier GetTierByRating(decimal rating)
    {
        foreach (RatingTier rt in ratingTiers.OrderByDescending(o => o.minimumRating))
            if (rt.TestForRating(rating))
                return rt;

        return ratingTiers.First();
    }

    void ShowNewspaper(RatingTier rt)
    {
        articleText.text = rt.GetRandomText();
        articleTitle.text = rt.GetRandomArticle();
        articleImage.sprite = images[Random.Range(0, images.Length)];
    }
}
