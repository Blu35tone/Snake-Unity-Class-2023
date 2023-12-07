using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Snake_Move : MonoBehaviour
{
    //variables
    private Vector2 direction;      //control direction of movement
    public bool goingUp;    //variables to keep track of up/down move       
    public bool goingDown;        
    public bool goingLeft;  //variables to keeo track of left/right move
    public bool goingRight;

    List<Transform> segments;       //variable to store all the parts of the body of the snake
    public Transform bodyPrefab;    //variable to store the body

    // Start is called before the first frame update
    void Start()
    {
        segments = new List<Transform>();      //create a new list
        segments.Add(transform);               //add the head of the snake to the list 
    }

    // Update is called once per frame
    void Update()
    {
        //change direction of the snake 
        if (Input.GetKeyDown(KeyCode.W) && goingDown !=true) //when W key is pressed
        {
            direction = Vector2.up; //go up
            goingUp = true;      
            goingDown = false;
            goingLeft = false;
            goingRight = false;
        }
        if (Input.GetKeyDown(KeyCode.S) && goingUp != true) //when S key is pressed
        {
            direction = Vector2.down; //go down
            goingUp = false;
            goingDown = true;
            goingLeft = false;
            goingRight = false;
        }
        if (Input.GetKeyDown(KeyCode.A) && goingRight != true) //when A key is pressed
        {
            direction = Vector2.left; //go left
            goingUp = false;
            goingDown = false;
            goingLeft = true;
            goingRight = true;
        }
        if (Input.GetKeyDown(KeyCode.D) && goingLeft != true) //when D key is pressed
        {
            direction = Vector2.right; //go right
            goingUp = false;
            goingDown = false;
            goingLeft = true;
            goingRight = true;
        }
    }


    //FixedUpdate is called at a fix interval
    void FixedUpdate()
    {
        //move the body of the snake
        for (int i = segments.Count - 1; i > 0; i--)                    //for each segment of the snake 
        {
            segments[i].position = segments[i - 1].position;            //move the body
        }

        //move the snake
        this.transform.position = new Vector2(                           //get the position
            Mathf.Round(this.transform.position.x) + direction.x,       //round the number add value to x
            Mathf.Round(this.transform.position.y) + direction.y       //round the number add value to y (don't add the ,
        );
    }
    
    //Function to make the snake grow 
    void Grow()
    {
        Transform segment = Instantiate(this.bodyPrefab);           //create a new body part
        segment.position = segments[segments.Count - 1].position;    //position it on the back of the snake
        segments.Add(segment);                                  //add it to the list
    }

    //Function for collision
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Food")        //checks if the other object is food
        {
            Grow(); //turn the grow function
            Time.fixedDeltaTime -= 0.0005f;
        }
        else if (other.tag=="Wall")     //checks if the other object is an obstacle/wall 
        {
            //Debug.Log("Hit");
            //SceneManager.LoadScene("DeathScene");   //change to the end scene
            SceneManager.LoadScene("GameScene"); //restart the game

        }
    }
}
