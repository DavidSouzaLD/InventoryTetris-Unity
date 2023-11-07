<h1>Inventory Tetris</h1>

<h2>Summary</h2>
<ol>
<li>Introduction</li>
<li>Starting</li>
</ol>

<h3>Introduction</h3>
<p>
Hi, welcome to an attempt to document the implementation of the
inventory system in your project!!
Feel free to contact me if something goes wrong or if you have any good
ideas to implement in the project. :)
</p>

<h3>Starting</h3>
<p> 
First, we will need to create a "Canvas" in Unity.
Click with the right mouse button to open the Unity menu and create a new canvas, follow the image as an example:
</p>

![Sem título](https://github.com/DavidSouzaLD/InventoryTetris-Unity/assets/100738882/121cf6b8-c0b7-4e81-a663-59681a91533f)
<p> 
After adding the Canvas, we will create a new object within the canvas and name it as we wish, in my case I will use "Inventory", because this will be the object responsible for keeping the main "Inventory" script.

Right after creating, we need to set a configuration in the "RectTransform" component of our new object.

Hold Shift + Alt and change the stretch, that way:
</p>

![Sem título2](https://github.com/DavidSouzaLD/InventoryTetris-Unity/assets/100738882/cb9c55f3-1242-460f-aa3f-c2e48dc6cee1)

<p>
I'm going to add a camera to the scene, so we don't get that "No camera rendering" warning.

Let's also add another object of type "Image" inside our new Inventory object, I'll call it "Backpack [6x3]",
I will use the name backpack because this object will be responsible for being our “Backpack” and the values ​​that come after will be the slot numbers that our backpack will have.
</p>

![Sem título3](https://github.com/DavidSouzaLD/InventoryTetris-Unity/assets/100738882/427f4d34-9177-4af3-82cf-c164ecd424e8)

<p>
Now let's do some simple math, as I want my backpack to be 6x3 we need to calculate each separate value using the slot size as the second value.
</p>

<p>
I will use the 96x96 slot size, the same as the one I use in my current project.
</p>

It will look like this:
<ul>
<li>Width: 6x96 = 576</li>
<li>Height: 3x96 = 288</li>
</ul>

<p>
I changed the color of the image component too, after these changes we should have something similar to this:
</p>

![image](https://github.com/DavidSouzaLD/InventoryTetris-Unity/assets/100738882/8a3706ec-84c8-42d4-88a3-17902d003de9)
