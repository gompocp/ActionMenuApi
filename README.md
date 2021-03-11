# ActionMenuApi <br>
> Currently in the process of rewriting the old system of this <br>Old version info available further down and source code in the releases section <br>

![alt text](https://cdn.discordapp.com/attachments/761897291388157955/789496482092679168/unknown.png)

Now supports the <br>
- Radial puppet (broken in vr oops :) )
- Button
- Toggle 
- SubMenu  
You can currently add toggles and buttons custom sub menus

Working on
- FourAxisPuppet
- Correct placement of the radial puppet

# Usage

The test mod I've included should be more than enough but I'll add these here anyway


```cs
//Add a reference to the api dll and add this to the top of your file
using ActionMenuApi;

//To add a button to the main page of the action menu
AMAPI.AddButtonPedalToMenu(ActionMenuPageType.Main, () => MelonLogger.Msg("Pressed Button") , "Button", buttonIcon);

//To add a toggle to the main page of the action menu
AMAPI.AddTogglePedalToMenu(ActionMenuPageType.Main, testBool, b => testBool = b, "Toggle", toggleIcon);

//To add a radial pedal to the main page of the action menu
AMAPI.AddRadialPedalToMenu(ActionMenuPageType.Main, f => testFloatValue = f, "Radial", testFloatValue, radialIcon);

//To add a submenu to the main page of the action menu and add a toggle and button to it
AMAPI.AddSubMenuToMenu(ActionMenuPageType.Main, 
    delegate {
        MelonLogger.Msg("Sub Menu Opened");
        AMAPI.AddButtonPedalToSubMenu(() => MelonLogger.Msg("Pressed Button In Sub Menu"), "Sub Menu Button", buttonIcon);
        AMAPI.AddTogglePedalToSubMenu(b => testBool2 = b, testBool2, "Sub Menu Toggle", toggleIcon);
    },
    "Sub Menu", 
    subMenuIcon
);
```



# Credits
- Assetbundle loading mechanism from Knah
- Icons from https://uxwing.com/



# Old Version Info
- I haven't tested everything because effort lol. If something is broken open up an issue or fix it yourself ¯\\_(ツ)_/¯ 
- Made by gompo#6956 you are free to use this provided credit is given<br>
  your mod is licensed under the same license as the one in this repository <br>
  for more complicated situations please dm me on discord<br>
-Credits:  <br>
  -Function XRefCheck here adapted to use string lists rather than just strings from Ben's <br>
  (Ben 🐾#3621) Dynamic Bone Safety Mod, link: https://github.com/BenjaminZehowlt/DynamicBonesSafety/blob/master/DynamicBonesSafetyMod.cs<br>


- There are only 4 Functions you need to be aware of
<ol>
  <li>
<h2>ActionMenuApi()</h2>
  <p>
    Constructor. Used to create new ActionMenuApi Instance and applies all neccessary patches 
  </p>
  <pre><code class='language-cs'>
  ActionMenuApi actionMenuApi = new ActionMenuApi();
  </code></pre>
  </li>
<li><h2>AddPedalToExistingMenu()</h2>
  <p>
    Adds a custom Pedal to an already existing menu in vrchat. Options are in enum ActionMenuPageType
  </p>
  <p>
    Params are ActionMenuPageType, Action(onClick), string(Pedal Text), Texture2D(Pedal Texture), Insertion(Insert before or after VRChat)
  </p>
  <pre><code class='language-cs'>
  actionMenuApi.AddPedalToExistingMenu(ActionMenuApi.ActionMenuPageType.Options, new Action(delegate
  {
    MelonLogger.Log("Pedal Pressed");                  
  }), "Example Button", icon, ActionMenuApi.Insertion.Post);
  </code></pre>
  </li>
<li><h2>CreateSubMenu</h2><p>
    call this in a triggerevent for an already existing pedal to open a new submenu
  </p>
  <p>
    Add pedals to this in the openFunc param using AddPedalToCustomMenu()
  </p>
  <pre><code class='language-cs'>
  actionMenuApi.CreateSubMenu(new Action(delegate {
    //actionMenuApi.AddPedalToCustomMenu() etc..
  }));
  </code></pre>
  </li>
<li><h2>AddPedalToCustomMenu</h2><p>
    If you create a custom Submenu you can use this to add your pedals to it
  </p>
  <pre><code class='language-cs'>
  actionMenuApi.AddPedalToCustomMenu(new Action(delegate {
    //Invoke Method or whatever here
  }), "DoSomething", coolIcon);
  </code></pre></li>
  </ol>
