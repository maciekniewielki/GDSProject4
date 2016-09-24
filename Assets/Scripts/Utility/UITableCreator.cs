using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UITableCreator : MonoBehaviour
{
    public RectTransform tableParent;
    public RectTransform cellPrefab;

    public GameObject tableRowPrefab;


    private Table tableToDisplay;


    void Start()
    {
        Table t = new Table(17, 3);
        t.SetHeader(new string[] { "ID", "Goals", "Tackles", "ID", "Goals", "Tackles", "ID", "Goals", "Tackles", "ID", "Goals", "Tackles", "ID", "Goals", "Tackles", "ID", "Goals" });
        t.SetRow(1,new string[]{"1", "2", "3", "1", "2", "3", "1", "2", "3", "1", "2", "3", "1", "2", "3", "1", "2"});
        t.SetRow(2, new string[]{ "2", "5", "15", "2", "5", "15", "2", "5", "15", "2", "5", "15", "2", "5", "15", "2", "5"});
        CreateTableWithData(t, tableParent);
    }

    public void CreateTableWithRect(Table table, GridLayoutGroup parent)
    {
        Rect parentRect = parent.GetComponent<RectTransform>().rect;
        Vector2 cellSize = new Vector2(parentRect.width / table.Width, parentRect.height / table.Height);
        Vector2 spacing = cellSize*-0.03f;
        parent.cellSize = cellSize - new Vector2(spacing.x, spacing.y);
        parent.spacing = spacing;
        parent.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
        parent.constraintCount = table.Width;
        for (int ii = 0; ii < table.Width; ii++)
        {
            for (int jj = 0; jj < table.Height; jj++)
            {
                RectTransform rect = Instantiate(cellPrefab) as RectTransform;
                rect.SetParent(tableParent.transform);
                rect.localScale = new Vector3(1f, 1f, 1f);
                Text cellText = rect.GetComponentInChildren<Text>();
                cellText.text = table.data[ii][jj];
            }
        }
    }

    public void CreateTableWithData(Table data, RectTransform parent)
    {
        for (int ii = 0; ii < data.Height; ii++)
            AddRowInDynamicTable(parent, ii, data.data[ii]);
    }


    private void AddRowInDynamicTable(RectTransform parent, int rowNumber, string[] data)
    {
        Rect parentRect = parent.rect;
        GameObject row = Instantiate(tableRowPrefab) as GameObject;
        row.transform.SetParent(parent.transform);
        RectTransform rowTransform = row.GetComponent<RectTransform>();
        rowTransform.anchoredPosition = new Vector2(0f, -tableRowPrefab.GetComponent<RectTransform>().rect.height * rowNumber);
        rowTransform.localScale = Vector3.one;
        foreach (Text field in rowTransform.GetComponentsInChildren<Text>())
        {
            int index = int.Parse(field.name.Substring(field.name.Length - 2, 2));
            field.text = data[index];
        }
    }
}
