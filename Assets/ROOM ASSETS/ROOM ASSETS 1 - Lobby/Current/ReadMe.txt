Thank you for purchasing Algorithmic Lightning Generator! Utilizing this asset, you can create a wide variety of lightning effects.

Just drop the prefab, textures, script, and materials into your scene and use it as you would any other particle system.

For questions or support, please contact Help@BinarySystemGames.com

 

What follows is a description of each of the parameters in the script, and suggestions on how to use them.

 

My Target: The GameObject to which the lightning bolts will travel. Placing it in the same location as the lightning prefab will cause the bolts to radiate in all directions if spreading is turned on.

Lightning Material: The material which is used to draw the lightning. Controls the look and color of the bolts.

Glow Material: The material used to draw the glow around the lightning. It should be a very light color, or very low alpha on it.

Redraw Speed: The speed in seconds at which the lightning bolts are updated. 0.04-0.05 give a good "realistic" electric effect.

Three D: The lightning is capable of rendering in 3D or 2D, controlled by this checkbox.

Glow: Controls whether a second bolt is drawn behind the first, slightly larger, and blended with it. Many different visual effects and colors can be created with Glow on.

Lightning Step Size: Controls the size of each segment of the lightning bolt.

Lightning Step Radius: Controls the distance each segment of the lightning will look for it's next point. For best performance, keep Lightning Step Size and Lightning Step Radius at the same values.

Lightning Branches: The maximum number of branches the lightning is able to make. 1-20 is a suggested range for excellent effects and best performance. Do not edit this parameter at runtime.

Branch Potential: The percent chance that on any given step the lightning will form a branch, up to the value of Lightning Branches. A 0-1 value. Using a value closer to 1 will cause the lightning to branch early, making most of the branches from the top of the bolt. A very small chance, such as 0.005, will cause the lightning to branch closer to the target.
Lightning Size Start Main: The starting width of the main branch of the lightning. The main branch is the one which always seeks My Target.

Lightning Size End Main: The end width of the main branch of the lightning. Setting to 0 will cause the lightning to fade out as it reaches My Target. Setting higher than Lightning Start Size Main will cause the lightning to grow as it approaches My Target.

Lightning Size Start Branch: The starting width of secondary lightning branches. For best visual effect, it is suggested to use half the size of the main branch.

Lightning Size End Branch: The ending width of secondary lightning branches. Setting this to 0 will cause the branches to fade out as they end.

Full Branch Is Main: Setting this checkbox will cause one main branch which moves towards My Target from which all secondary branches begin. Turning this off will allow any secondary branch to spawn the branch which eventually reaches My Target. If you want the lightning to zigzag around before reaching My Target, turn this off.

Branches Spreading: Allows secondary branches to move away from the main branch. Turning this off causes all secondary branches to move towards My Target. To get an effect where many branches all converge on a single point, turn this off.

Spread From Branches: Allows secondary branches to spread relative to themselves instead of My Target. Turning this on will allow secondary branches to move farther away from the main branch.

Treed Branches: Forces all secondary branches to spawn off of each other instead of the main branch, creating a tree-like effect.

Branch Spread Radius: The distance that secondary branches are allowed to wander from My Target.

Branch Shortening: Cuts off secondary branches before they reach their destination. A 0-1 value. 0 will cause no branches to spawn and 1 will apply no shortening. 0.5 is recommended.

Glow Range: The distance the glow will spread from the branches. It is a multiplier of the branch width. 2 is recommended for best visual performance.


Try combining multiple sets of Lightning to create complex effects!