# unity-sliding-text
Unity Text UI that moves automatically from side to side.  

## Purpose
If the string is long and appears to be cut, You can extend width of the RectTransform by applying the **Content Size Fitter**, Adjust the font size with **BestFit**. But you may not like either way.  
For example, when you have to display long text to a specified width.
  
## Feature
- You can change both the forward and backward scroll speed. Or you can move text to the front immediately.
- Only Unity UI elements used. (No DoTween, No TextMeshPro)
- Simple and commented script. (~200 lines)
  
## Unity Version
2019.4.40f1
  
## How to use
1. Create Panel
2. Add Component 'SlidingText'. Then other components will be added automatically via script.
3. Adjust font size of the Text component, Edit text string, ... etc.
4. Extend the RectTransform of Text longer than the parent (Panel we created before).
  
## Screenshot
![Screenshot](https://github.com/virtus2/unity-sliding-text/blob/main/Screenshot/sliding-text.gif)
  
## Things to improve further
- Support TextMeshPro
- Use Linear interpolation
  
## Contact
khd1323@naver.com
  
