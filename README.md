<h1>Inventory Tetris</h1>

<p>
A Tetris-style inventory system for Unity would feature a grid where players organize items with various shapes into an efficient layout. Items must be rotated and strategically placed to optimize space, much like fitting Tetris blocks together
</p>

## Summary

[1. Introduction](#introduction)

[2. Starting](#starting)

[3.Creating Inventory](#creating-inventory)

[4.Creating Items](#creating-items)

[5.Testing The Inventory](#testing-the-inventory)

### Introduction
<p>
Hi, welcome to an attempt to document the implementation of the
inventory system in your project!!
Feel free to contact me if something goes wrong or if you have any good
ideas to implement in the project. :)
</p>

### Starting
<p> 
First open the "Inventory" script, go to the first lines where there is the static class "InventorySettings" and choose the desired options for your inventory.
<p> 

![image](https://github.com/DavidSouzaLD/InventoryTetris-Unity/assets/100738882/f85f3490-cd7d-4a33-8094-cba7cd4c8e6d)


<p>
Then we need to create a "Canvas" in Unity.

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

It will look like this:
<ul>
<li>Width: 6x96 = 576</li>
<li>Height: 3x96 = 288</li>
</ul>

I changed the color of the image component too, after these changes we should have something similar to this:
</p>

![image](https://github.com/DavidSouzaLD/InventoryTetris-Unity/assets/100738882/8a3706ec-84c8-42d4-88a3-17902d003de9)

<p>
Let's create another object called "Grid", this object will be a child of the "Backpack [6x3]" object and every backpack object needs a grid object.

In the grid object, I will also add an image element.

> [!IMPORTANT]
IMPORTANT POINT: Use Shift + Alt to change your Stretch to the top left corner.
</p>

![image](https://github.com/DavidSouzaLD/InventoryTetris-Unity/assets/100738882/aedce17d-7616-46ed-83c5-5be4de97a891)

### Creating Inventory
<p>
Now we get to the best part, adding scripts and building our inventory.

We will need to add 2 scripts to our Inventory object.
They are:

<ul>
<li>Inventory</li>
<li>InventoryController</li>
</ul>

Later on I'll talk more about each one, for now let's try to make it work first.
</p>

![image](https://github.com/DavidSouzaLD/InventoryTetris-Unity/assets/100738882/03d01a58-4eac-4944-a2b1-f474ae568345)

<p>
Now let's add the "InventoryGrid" script to our "Backpack [6x3]" object.
</p>

![image](https://github.com/DavidSouzaLD/InventoryTetris-Unity/assets/100738882/c10e2f9b-c597-47db-b446-b4b0ad60aff4)

<p>
Now that we have the necessary scripts, let's add the variables for each one.

We already have the necessary scripts, let's add our "Grid" object to the InventoryGrid variable "RectTransform", like this:
</p>

![Sem título4](https://github.com/DavidSouzaLD/InventoryTetris-Unity/assets/100738882/4071416c-31ea-4042-8008-6596c6b36716)

<p>
And let's set the grid size to the size of our backpack, in my case 6x3.
</p>

![image](https://github.com/DavidSouzaLD/InventoryTetris-Unity/assets/100738882/84649d65-76e2-45e2-b469-5e0a8fa59fed)

<p>
Alright, now we just need to place a sprite of the size we want our slot to have in our "Grid" object in the "Image" component, after we add the sprite, let's go under the "Image" component in the "ImageType" option and select the Tilled option.

I'll leave the image I use in my project so you can get an idea of ​​how it works.
</p>

<p>
Our grid object will look like this:
</p>

![image](https://github.com/DavidSouzaLD/InventoryTetris-Unity/assets/100738882/a656a012-e3fd-405b-ab52-ca6743d436b6)

### Creating Items
<p>
Now let's create some items to test our inventory :)

To create an item we will need to create a folder called "Items" or something similar, after creating it, right click inside it and look for the "Create/InventoryTetris/ItemData" option.

It will look something like this image:
</p>

![Sem título5](https://github.com/DavidSouzaLD/InventoryTetris-Unity/assets/100738882/e16f2eed-1a5d-473e-ba0f-d4690af52046)

<p>
The item name is defined by the name of the ScriptableObject. After choosing the name, we will choose how our object will work.

Select a good photo for the item and choose the size it will use from the inventory.
</p>

![image](https://github.com/DavidSouzaLD/InventoryTetris-Unity/assets/100738882/c8cb2bdf-93fc-47a1-8432-49d616cdb09d)

<p>
Let's go to the last step now, creating the prefabricated object.

<ul>
<li>Create a new empty object inside the canvas and name it "ItemPrefab", go to "RectTransform" and select the width and height of the slot size that you configured in "InventorySettings".</li>
<li>Then create two more image objects, name one "Icon" and the other "Background".</li>
<li>Leave the two image-type objects you just created with the maximized stretch the same as we did a while ago.</li>
<li>Add the "Item" script to the object we just created and place the "Icon" and "Background" components and place the image components we just created in them.</li>
</ul>

</p>

![Sem título6](https://github.com/DavidSouzaLD/InventoryTetris-Unity/assets/100738882/67ea93b7-f2af-4b02-a92a-c6d140f14bc6)

<p>
Now save this item we just created in some folder, I recommend creating a folder called "Prefabs" and put it there
</p>

![image](https://github.com/DavidSouzaLD/InventoryTetris-Unity/assets/100738882/1b44ce83-23d1-4734-b77a-dbccfed92c91)

### Testing The Inventory
<p>
  Now we have almost everything to start using our inventory.

  Let's drag our "ItemPrefab" to the "Inventory" script and place it in the corresponding variable.
</p>

![Sem título7](https://github.com/DavidSouzaLD/InventoryTetris-Unity/assets/100738882/f520dbef-3243-474f-9d5f-1d63812ea5bc)

<p>
And we will also add the data of the items we created previously to the "Inventory" script, like this:
</p>

![image](https://github.com/DavidSouzaLD/InventoryTetris-Unity/assets/100738882/a5ec33e7-0d1a-4bee-9627-c10f4012d492)

<p>
Now let's test our inventory :)

but

First of all, you can change the buttons in the "InventoryController" script, in addition, all the code is commented and written semantically.

If you have any ideas, don't hesitate to try!!
Thank you for your patience and follow the video of our inventory working.
</p>

https://github.com/DavidSouzaLD/InventoryTetris-Unity/assets/100738882/c4e7d658-c2d9-460d-833e-1079066e97f7

https://github.com/DavidSouzaLD/InventoryTetris-Unity/assets/100738882/3af77012-27dc-4489-8972-ec2e4bf83acb

https://github.com/DavidSouzaLD/InventoryTetris-Unity/assets/100738882/87ad7602-934e-433d-b9bf-6ca447e6439a
