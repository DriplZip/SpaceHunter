using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
   [Header("Set in Inspector")]
   public GameObject PlayerGameObject;
   public GameObject[] panels;
   public float scrollSpeed = 30f;
   public float motionMult = 0.25f;

   private float panelHeight;
   private float depth;

   private void Start()
   {
      panelHeight = panels[0].transform.localScale.y;
      depth = panels[0].transform.position.z;

      panels[0].transform.position = new Vector3(0, 0, depth);
      panels[1].transform.position = new Vector3(0, panelHeight, depth);
   }

   private void Update()
   {
      float tX = 0;
      float tY = Time.time * scrollSpeed % panelHeight ;

      if (PlayerGameObject != null) tX = -PlayerGameObject.transform.position.x * motionMult;

      panels[0].transform.position = new Vector3(tX, tY, depth);

      if (tY >= 0)
      {
         panels[1].transform.position = new Vector3(tX, tY - panelHeight, depth);
      }
      else
      {
         panels[1].transform.position = new Vector3(tX, tY + panelHeight, depth);
      }
   }
}
