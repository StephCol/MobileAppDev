# Games with Mobile Devices - Touch Assignment

This project uses touch controls for camera control on a touchscreen device, and to manipulate game objects.

## Installation

1. Attach **SceneGenerator.cs** to an empty game object
2. Create **Event System** to ensure RESET button events are efficiently dealt with. To find the Event System object select the following: 
    * Game Object > UI > Event System


## Usage

1. **Game Object Controls**
   - **Tap** object to select (object is blue when selected). Tap again to *deselect*, or alternatively tap elsewhere on the scene.
   - **Drag** object to the desired position by selecting the object and moving your finger across the screen
   - **Rotate & Resize** selected object by using a two-finger pinch


2. **Camera Controls** -  *deselect any game object to use camera controls*
   - **Zoom** using two fingers, pinch the screen to zoom in/out 
   - **Rotate** using two fingers, move around the screen to rotate the camera
   - **Pan** drag a single finger in the desired direction to strafe/pan the camera
   - **Double Tap** to reset the camera to it's starting position

3. **Reset Game** by selecting the reset button
