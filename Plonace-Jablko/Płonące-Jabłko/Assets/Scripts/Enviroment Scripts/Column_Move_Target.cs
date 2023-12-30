using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Column_Move_Target : MonoBehaviour
{
    public bool Left;
    public bool Right;
    public bool Up;
    public bool Down;
    Column_Pushable column_pushable;
    float Grid_Size;
    LayerMask Layer_Columns_Move_On;

    private void Start()
    {
        column_pushable = gameObject.GetComponentInParent<Column_Pushable>();
        Grid_Size = column_pushable.Grid_Size;
        Layer_Columns_Move_On = column_pushable.Layer_Columns_Move_On;
    }

    void Update()
    {
        transform.parent = null;
        if (Left)
        {
            Left = false;
            gameObject.transform.position += Vector3.left * Grid_Size;
            if(!Physics2D.OverlapCircle(gameObject.transform.position, Grid_Size * 0.1f, Layer_Columns_Move_On)) 
            {
                gameObject.transform.position -= Vector3.left * Grid_Size;
            }
        }
        if (Right)
        {
            Right = false;
            gameObject.transform.position += Vector3.right * Grid_Size;
            if (!Physics2D.OverlapCircle(gameObject.transform.position, Grid_Size * 0.1f, Layer_Columns_Move_On))
            {
                gameObject.transform.position -= Vector3.right * Grid_Size;
            }
        }
        if (Up)
        {
            Up = false;
            gameObject.transform.position += Vector3.up * Grid_Size;
            if (!Physics2D.OverlapCircle(gameObject.transform.position, Grid_Size * 0.1f, Layer_Columns_Move_On))
            {
                gameObject.transform.position -= Vector3.up * Grid_Size;
            }
        }
        if (Down)
        {
            Down = false;
            gameObject.transform.position += Vector3.down * Grid_Size;
            if (!Physics2D.OverlapCircle(gameObject.transform.position, Grid_Size * 0.1f, Layer_Columns_Move_On))
            {
                gameObject.transform.position -= Vector3.down * Grid_Size;
            }
        }
    }
}
