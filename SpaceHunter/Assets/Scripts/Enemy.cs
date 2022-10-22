using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Enemy : MonoBehaviour
{
   [Header("Set in Inspector")]
   [SerializeField] private float speed = 10f;
   [SerializeField] private float fireRate = 0.3f;
   [SerializeField] private float health = 10f;
   [SerializeField] private int score = 100;

   public Vector3 Position
   {
      get => transform.position;
      private set => transform.position = value;
   }

   private void Update()
   {
      Move();
   }

   protected virtual void Move()
   {
      Vector3 tmpPosition = Position;

      tmpPosition.y -= speed * Time.deltaTime;

      Position = tmpPosition;
   }
}
