# ActionMenuApi <br>
- I haven't tested everything because effort lol. If something is broken open up an issue or fix it yourself ¯\_(ツ)_/¯ 
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
