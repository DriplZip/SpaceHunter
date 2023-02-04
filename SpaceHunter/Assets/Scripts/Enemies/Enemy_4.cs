using System;
using UnityEngine;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

[System.Serializable]
public class Part
{
    public string name;
    public float health;
    public string[] protectedBy;

    [HideInInspector] public GameObject gameObjectPart;
    [HideInInspector] public Material materialPart;
}

public class Enemy_4 : Enemy
{
    [Header("Set in Inspector: Enemy_4")] public Part[] parts;
    
    private Vector3 _p0, _p1;
    private float _timeStart;
    private float _duration = 4;

    private void Start()
    {
        _p0 = _p1 = Position;
        
        InitMovement();

        Transform t;
        foreach (Part part in parts)
        {
            t = transform.Find(part.name);
            if (t != null)
            {
                part.gameObjectPart = t.gameObject;
                part.materialPart = part.gameObjectPart.GetComponent<Renderer>().material;
            }
        }
        
    }

    void InitMovement()
    {
        _p0 = _p1;

        float widthMinRad = _bordersCheck.CamWight - _bordersCheck.RepulsionRadius;
        float heightMinRad = _bordersCheck.CamHeight - _bordersCheck.RepulsionRadius;
        _p1.x = Random.Range(-widthMinRad, widthMinRad);
        _p1.y = Random.Range(-heightMinRad, heightMinRad);

        _timeStart = Time.time;
    }

    protected override void Move()
    {
        float u = (Time.time - _timeStart) / _duration;

        if (u >= 1)
        {
            InitMovement();
            u = 0;
        }

        u = 1 - Mathf.Pow(1 - u, 2);
        Position = (1 - u) * _p0 + u * _p1;
    }

    Part FindPart(string n)
    {
        foreach (Part part in parts)
        {
            if (part.name == n) return part;
        }

        return null;
    }

    Part FindPart(GameObject go)
    {
        foreach (Part part in parts)
        {
            if (part.gameObjectPart == go) return part;
        }

        return null;
    }
    
    bool Destroyed(Part part)
    {
        if (part == null) return true;

        return part.health <= 0;
    }

    bool Destroyed(string n)
    {
        return Destroyed(FindPart(n));
    }

    bool Destroyed(GameObject go)
    {
        return Destroyed(FindPart(go));
    }

    void ShowLocalizedDamage(Material m)
    {
        m.color = Color.red;
        _damageDoneTime = Time.time + showDamageDuration;
        _showingDamage = true;
    }

    void OnCollisionEnter(Collision collision)
    {
        GameObject other = collision.gameObject;
        switch (other.tag)
        {
            case "ProjectileHero":
                Projectile p = other.GetComponent<Projectile>();
                if (!_bordersCheck.IsOnScreen)
                {
                    Destroy(other);
                    break;
                }

                GameObject goHit = collision.contacts[0].thisCollider.gameObject;
                Part partHit = FindPart(goHit);
                if (partHit == null)
                {
                    goHit = collision.contacts[0].otherCollider.gameObject;
                    partHit = FindPart(goHit);
                }

                if (partHit.protectedBy != null)
                {
                    foreach (string s in partHit.protectedBy)
                    {
                        if (!Destroyed(s))
                        {
                            Destroy(other);
                            return;
                        }
                    }
                }

                partHit.health -= Main.GetWeaponDefinition(p.WeaponType).damageOnHit;
                ShowLocalizedDamage(partHit.materialPart);
                
                if (partHit.health <= 0) partHit.gameObjectPart.SetActive(false);

                bool allDestroyed = true;
                foreach (Part part in parts)
                {
                    if (!Destroyed(part))
                    {
                        allDestroyed = false;
                        break;
                    }
                }

                if (allDestroyed)
                {
                    Main.S.ShipDestroyed(this);
                    Destroy(this.gameObject);
                }
                
                Destroy(other);
                break;
        }
    }
}