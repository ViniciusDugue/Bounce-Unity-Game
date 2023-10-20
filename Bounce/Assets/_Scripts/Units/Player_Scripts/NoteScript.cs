// // public void DrawBallTrajectoryLines()
// // {
// //     float distanceFromBall = Vector3.Distance(player.transform.position, ball.transform.position);
// //     float maxLineDistance  = 20.0f;
// //     float currentLineDistance = 0.0f;
// //     int trajectoryBounces = 1;
// //     if (distanceFromBall <= ballShootRadius && !isDead)
// //     {
// //         Vector3 ballPosition = ball.transform.position;
// //         Vector3 mousePosition = playerCamera.ScreenToWorldPoint(Input.mousePosition) - new Vector3(0, 0, playerCamera.ScreenToWorldPoint(Input.mousePosition).z);
// //         Vector3 ballDirection = (mousePosition - ballPosition).normalized;

// //         //cast ray from ball to first collider
// //         RaycastHit2D hit = Physics2D.Raycast(ballPosition, ballDirection, maxLineDistance - currentLineDistance, wallLayerMask);
// //         Vector3 wallNormal = hit.normal;
// //         Vector3 collisionPosition = hit.point;
// //         float lineLength = (collisionPosition - ballPosition).magnitude;
// //         currentLineDistance += lineLength;
// //         // draw line from ball to first collider
// //         Debug.DrawLine(ballPosition, collisionPosition, Color.red);

// //         //if the first collision exists
// //         if (hit.collider != null)
// //         {   
// //             // while there is still distance left for trajectory line to be drawn
// //             while( currentLineDistance <maxLineDistance && trajectoryBounces <=6)
// //             {
// //                 lineLength = (collisionPosition - ballPosition).magnitude;
// //                 // calculate the reflected direction of previous line
// //                 Vector3 reflectedDirection = Vector3.Reflect(ballDirection, wallNormal).normalized;
// //                 //update new ballposition and collisionposition after ray casting to find new collision
// //                 ballPosition = collisionPosition;
// //                 // cast a ray in reflected direction until hit collider and store new collider pos
// //                 hit = Physics2D.Raycast(ballPosition, reflectedDirection, maxLineDistance - currentLineDistance, wallLayerMask);
// //                 wallNormal = hit.normal;
// //                 //update new collisionposition and balldirection after ray casting to calculate drawlines
// //                 collisionPosition = hit.point;
// //                 ballDirection = (collisionPosition - ballPosition).normalized;
// //                 // draw the line from ballposition to collider position
// //                 if(currentLineDistance + lineLength <maxLineDistance)
// //                 {
// //                     Debug.DrawLine(ballPosition, collisionPosition, Color.red);
// //                 }
// //                 else
// //                 {
// //                     float leftOverLineDistance = maxLineDistance - currentLineDistance;
// //                     Debug.DrawLine(ballPosition, ballDirection * leftOverLineDistance, Color.red);
// //                 }
// //                 trajectoryBounces+=1;
// //                 currentLineDistance += lineLength;  
// //             }
            
// //         } 
        
// //     }
// // }


// public void DrawBallTrajectoryLines()
// {
//     // 1st line: ray cast from ball position following shoot direction until it hits an obstacle and store that collision position. draw a line until it hits the max trajectory line distance. If not then go to next while loop
//     // while trajectory line distance < max trajectoryline distance, ray cast 
//     // 2nd line: cast ray from ball in shoot direction and calculate second line by reflecting the 
//     // shoot direction vector off the wall angle 
    
//     //recursively cast rays from a previous collision
//     float distanceFromBall = Vector3.Distance(player.transform.position, ball.transform.position);
//     float maxLineDistance  = 1000.0f;
//     if (distanceFromBall <= ballShootRadius && !isDead)
//     {
        
//         Vector3 ballPosition = ball.transform.position;
//         // Debug.DrawLine(ballPosition, transform.position , Color.red);
//         Vector3 mousePosition = playerCamera.ScreenToWorldPoint(Input.mousePosition) - new Vector3(0, 0, playerCamera.ScreenToWorldPoint(Input.mousePosition).z);
//         Vector3 shootDirection = (mousePosition - ballPosition).normalized;

//         RaycastHit2D hit = Physics2D.Raycast(ballPosition, shootDirection, maxLineDistance, wallLayerMask);

//         if (hit.collider != null) // && hit.collider.CompareTag("Tilemap")
//         {
//             Vector3 collisionPosition = hit.point;
//             print("collisionposition:" + collisionPosition);
//             Vector3 wallNormal = hit.normal;
//             // float angle = Mathf.Atan2(wallNormal.y, wallNormal.x) * Mathf.Rad2Deg;
//             // Debug.Log("Angle of the wall: " + angle);

//             // Assuming you have a LineRenderer component attached to the ball object:
//             Debug.DrawLine(ballPosition, collisionPosition, Color.red);


//             // Calculate the reflected direction
//             Vector3 reflectedDirection = Vector3.Reflect(shootDirection, wallNormal);

//             // Draw the reflected line
//             // lineRenderer.SetPosition(1, ballPosition + reflectedDirection * maxDistance);
//             Debug.DrawLine(collisionPosition, collisionPosition + reflectedDirection, Color.red);
//         }
//     }
// }



