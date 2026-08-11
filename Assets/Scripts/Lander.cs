using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class Lander : MonoBehaviour
{
    private const float Gravity_Normal = 0.7f;
    
    public static Lander Instance { get; private set; }

    private List<KeyGiver> collectedKeys = new List<KeyGiver>();

    public event EventHandler<KeyGiver> onKeyCollected;
    public event EventHandler<KeyGiver> onKeyUsed;

    public event EventHandler onUpforce;
    public event EventHandler onRightforce;
    public event EventHandler onLeftforce;
    public event EventHandler onbeforeUpforce;
    public event EventHandler onCoinPickUp;
   
    public event EventHandler onFuelPickUp;

   
    public event EventHandler <onStateChangedEventArgs>onStateChanged;
    public class onStateChangedEventArgs: EventArgs
    {
        public State state;
    }
    public event EventHandler <onLandingEventArgs> onLanding;
    public class onLandingEventArgs: EventArgs
    {
        public LandingType landingtype;
        public int score;
        public float dotVector;
        public float landingSpeed;
        public float scoreMultiplier;
    }

    public enum LandingType 
    {
        Success,
        WrongLandingArea,
        TooSteepAngle,
        TooFastLanding,
        BulletHit,
    }

    public enum State
    {
        waitingToStart,
        normal,
        gameOver,
    }
    public State state; 

    private Rigidbody2D landerRigidBody2D;
    private float maxFuelAmount = 10f;
    private float fuelAmount;
    private Vector2 windForce;

    public Vector3 lastPickupPosition;
    private void Awake()
    {
        Instance = this;
        landerRigidBody2D = GetComponent<Rigidbody2D>();
        landerRigidBody2D.gravityScale = 0f;
        fuelAmount = maxFuelAmount;
        SetState(State.waitingToStart);
}
  

    private void FixedUpdate()
    {
        onbeforeUpforce?.Invoke(this, EventArgs.Empty);
        switch (state)
        {
            default:
            case State.waitingToStart:
                if (GameInput.instance.isUpLanderPressed()|| GameInput.instance.isLeftLanderPressed() || GameInput.instance.isRightLanderPressed())
                {
                    landerRigidBody2D.gravityScale = Gravity_Normal;
                    SetState(State.normal);
                }
                break;
            case State.normal:
                if (fuelAmount <= 0f)
                {
                    return;
                }
                landerRigidBody2D.AddForce(windForce);
                if (GameInput.instance.isUpLanderPressed() || GameInput.instance.isLeftLanderPressed() || GameInput.instance.isRightLanderPressed())
                {
                    ConsumeFuel();
                }
                if (GameInput.instance.isUpLanderPressed())
                {
                    float speed = 780f;
                    landerRigidBody2D.AddForce(speed * transform.up * Time.deltaTime);
                    onUpforce?.Invoke(this, EventArgs.Empty);
                }
                if (GameInput.instance.isLeftLanderPressed())
                {
                    float turnspeed = 100f;
                    landerRigidBody2D.AddTorque(turnspeed * Time.deltaTime);
                    onLeftforce?.Invoke(this, EventArgs.Empty);
                }
                if (GameInput.instance.isRightLanderPressed())
                {
                    float turnspeed = -100f;
                    landerRigidBody2D.AddTorque(turnspeed * Time.deltaTime);
                    onRightforce?.Invoke(this, EventArgs.Empty);
                }
                break;
                case State.gameOver: break;
        }
        

    }
    private void OnCollisionEnter2D(Collision2D collision2D)
    {
     

        if (!collision2D.gameObject.TryGetComponent(out LandingPad landingpad))
        {
            onLanding?.Invoke(this, new onLandingEventArgs
            {
                landingtype = LandingType.WrongLandingArea,
                score = 0,
                dotVector = 0f,
                landingSpeed = 0f,
                scoreMultiplier = 0
            });
            SetState(State.gameOver);
            return;
        }
        float softlandingvelocitymangnitude = 4f;
        float relativevelocitymagnitude = collision2D.relativeVelocity.magnitude;
        if ( relativevelocitymagnitude> softlandingvelocitymangnitude)
        {
            onLanding?.Invoke(this, new onLandingEventArgs
            {
                landingtype = LandingType.TooFastLanding,
                score = 0,
                dotVector = 0f,
                landingSpeed = relativevelocitymagnitude,
                scoreMultiplier = 0
            });
            SetState(State.gameOver);
            return;
        }

        float dotVector = Vector2.Dot(Vector2.up,transform.up);
        float mindotvector = 0.9f;
        if (dotVector < mindotvector)
        {
            onLanding?.Invoke(this, new onLandingEventArgs
            {
                landingtype = LandingType.TooSteepAngle,
                score = 0,
                dotVector = dotVector,
                landingSpeed = relativevelocitymagnitude,
                scoreMultiplier = 0
            });
            SetState(State.gameOver);
            return;
        }
        float maxscoreamountlandingangle = 100f;
        float scoredotvectormultiplier = 10f;
        float landinganglescore = maxscoreamountlandingangle - Mathf.Abs(dotVector-1f) * scoredotvectormultiplier * maxscoreamountlandingangle;
        float maxscoreamountlandingspeed = 100f;
        float landingspeedscore = (softlandingvelocitymangnitude - relativevelocitymagnitude) * maxscoreamountlandingspeed;
        int score= Mathf.RoundToInt((landinganglescore+landingspeedscore)*landingpad.GetScoreMultiplier());
        Debug.Log("Score = " + score);
        onLanding?.Invoke(this, new onLandingEventArgs
        {
            landingtype = LandingType.Success,
            score = score,
            dotVector = dotVector,
            landingSpeed = relativevelocitymagnitude,
            scoreMultiplier = landingpad.GetScoreMultiplier()
        });
        SetState(State.gameOver);

    }

    private void OnTriggerEnter2D(Collider2D collision2D)
    {
        Debug.Log("Trigger entered: ");
        if (collision2D.gameObject.TryGetComponent(out FuelPickup fuelpickup))
        {
            float fuelAddAmonut = 10f;
            fuelAmount += fuelAddAmonut;
            if (fuelAmount > maxFuelAmount)
            {
                fuelAmount = maxFuelAmount;
            }
            lastPickupPosition = fuelpickup.transform.position;
            onFuelPickUp?.Invoke(this,EventArgs.Empty);
            fuelpickup.DestroyItSelf();
        }
        if (collision2D.gameObject.TryGetComponent(out CoinPickUp coinpickup))
        {
            lastPickupPosition = coinpickup.transform.position;
            onCoinPickUp?.Invoke(this, EventArgs.Empty);
            coinpickup.DestroyItSelf();
        }
        if (collision2D.gameObject.TryGetComponent(out CannonBullet bullet))
        {
            onLanding?.Invoke(this, new onLandingEventArgs{ landingtype = LandingType.BulletHit });
            bullet.DestroyItSelf();
            SetState(State.gameOver);
            return;
        }
    }

    private void SetState(State state)
    {
        this.state = state;
        onStateChanged?.Invoke(this, new onStateChangedEventArgs{ state=state,});
    }
    private void ConsumeFuel()
    {
        float fuelConsumptionAmount = 1f;
        fuelAmount -= fuelConsumptionAmount * Time.deltaTime;
    }

    public float GetFuel()
    {
        return fuelAmount;
    }
    public float GetFuelAmountNormalized()
    {
        return fuelAmount/maxFuelAmount;  
    }
    public float GetSpeedX()
    {
        return landerRigidBody2D.linearVelocityX;
    }
    public float GetSpeedY()
    {
        return landerRigidBody2D.linearVelocityY;
    }
    public void GiveKey(KeyGiver keyGiver)
    {
        collectedKeys.Add(keyGiver);
        onKeyCollected?.Invoke(this, keyGiver);
    }
    public bool HasKey(KeyGiver keyGiver)
    {
        return collectedKeys.Contains(keyGiver);
    }
    public void UseKey(KeyGiver keyGiver)
    {
        collectedKeys.Remove(keyGiver);

        onKeyUsed?.Invoke(this, keyGiver);
    }

    public void SetWindForce(Vector2 Force)
    {
        windForce = Force;
    }
}
