# Multi Device Communication

## Communication Setup

We were responsible for the communication between devices and the setup of the passthrough scene. The first thing we had to do was to set up the Oculus Quest and ensure that the Quest Link is working. This gave us the opportunity to debug the code directly on the headset.

To connect the headset wirelessly to the laptop via Wi-Fi, we used Meta Quest Air Link. Here's how to set it up:

1. Install the [Oculus Desktop App](https://www.meta.com/de/quest/setup/?utm_source=www.meta.com&utm_medium=dollyredirect) on your PC.
2. Start the Quest Link through the headset menu.
3. Pair your PC via Air Link.
4. Confirm that the code displayed on the headset matches the one shown on the PC.

## Passthrough Configuration

We were also responsible for configuring passthrough, which allows viewing the environment in real-time with the headset. We used passthrough to overlay the real environment with the display of the REBA score.


To enable Passthrough:

1. Go to the "Hierarchy" tab and select "OVRCameraRig".
2. In the "Inspector" tab, follow these steps:
   - a. Under the "Tracking" section, choose "Stage" as the "Tracking Origin Type".
   - b. In the "Quest Features" section, navigate to the "General" tab. Choose "Supported" from the "Passthrough Support" list.
   - c. Under "Insight Passthrough", enable "Enable Passthrough" to initialize passthrough during app startup.
   - d. Click "Add Component", then select the "OVRPassthroughLayer" script.
   - e. In the "OVRPassthroughLayer" settings, set "Placement" to "Underlay" to enable background passthrough for physical world appearance.

4. Access the menu, go to "Window", then select "Rendering", and finally "Lighting".
5. On the "Environment" tab, locate "Skybox Material" and choose "None".
6. In the "Hierarchy" tab, expand "OVRCameraRig > TrackingSpace > CenterEyeAnchor". In the "Inspector" tab:
   - a. From the "Clear Flags" list, choose "Solid Color".
   - b. Adjust the "Background color" to black and set the alpha value to 0. You can also set the (R,G,B,A) values to (0, 0, 0, 0).

This setup allows you to utilize passthrough and overlay virtual 3D objects onto the physical world.

## Contributors:
- [Nassim Laaraj](https://github.com/Nassim795)
- [Nabil Akir](https://github.com/nabil-ak)
- [Ibtehal Al-Omari](https://github.com/ib1907)
