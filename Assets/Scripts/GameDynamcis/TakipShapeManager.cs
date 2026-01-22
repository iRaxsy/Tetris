using UnityEngine;

public class TakipShapeManager : MonoBehaviour
{
    private ShapeManager takipShape = null;

    private bool dibeDegdimi = false;

    public Color color = new Color(1f, 1f, 1f, .2f);

    public void TakipShapeOlusturFNC(ShapeManager gercekShape, BoardManager board)
    {
        if (!takipShape)
        {
            takipShape = Instantiate(gercekShape, gercekShape.transform.position, gercekShape.transform.rotation) as ShapeManager;

            takipShape.name = "TakipShape";

            SpriteRenderer[] tumSprite = takipShape.GetComponentsInChildren<SpriteRenderer>();

            foreach (SpriteRenderer sr in tumSprite)
            {
                sr.color = color;
            }
        }
        else
        {
            takipShape.transform.position = gercekShape.transform.position;
            takipShape.transform.rotation = gercekShape.transform.rotation;
        }

        dibeDegdimi = false;

        while (!dibeDegdimi)
        {
            takipShape.AsagiHareketFNC();

            if (!board.GecerliPozisyondami(takipShape))
            {
                takipShape.YukariHareketFNC();
                dibeDegdimi = true;
            }
        }
    }

    public void ResetFNC()
    {
        Destroy(takipShape.gameObject);
    }
}
