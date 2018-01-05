# PS4Macro.Remote

[![Twitter](https://img.shields.io/twitter/url/https/twitter.com/fold_left.svg?style=social&label=Follow%20Me)](https://twitter.com/itskomefai)
[![Donate](https://img.shields.io/badge/Donate-PayPal-green.svg)](http://paypal.me/Komefai)

Remap and use your keyboard to control PS4 Remote Play using [PS4 Macro](https://github.com/komefai/PS4Macro). This project will eventually be merged into a built-in feature of PS4 Macro.

## EDIT: This project has been merged and DISCONTINUED (See below)

Please use the "Remapper" feature in [PS4 Macro](https://github.com/komefai/PS4Macro) instead (Tools->Remapper). The feature is heavily based on this project and has been improved for better performance. Thanks to everyone who helped me with this project by reporting bugs and suggesting new features.

---

#### Screenshot

![Screenshot](https://raw.githubusercontent.com/komefai/PS4Macro.Remote/master/_resources/Screenshot.png)

#### Demo Video

[![Demo Video](https://img.youtube.com/vi/XrpiHAHSFeo/0.jpg)](https://www.youtube.com/watch?v=XrpiHAHSFeo)

## Usage

1. Open PS4Macro.Remote.dll in PS4 Macro.
2. Press play.
3. Focus PS4 Remote Play window.
4. Press the keys on your keyboard.

## Settings

To map a key to a button or a macro, edit the **Key** cell and enter your desire key. You can find the key from the **Member name** column in [this table](https://msdn.microsoft.com/en-us/library/system.windows.forms.keys(v=vs.110).aspx). Use `None` to disable the key.

To add a recorded macro, click on `...` to browse and select the file.

To use without a DualShock 4, set `<EmulateController>` to true in PS4 Macro's settings.xml file.

## To-Do List

- Detect key without input manually
