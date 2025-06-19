# CodeComparer – Code Difference Viewer

**CodeComparer** is a WPF desktop application for visually comparing differences between two blocks of source code. It highlights changes, shows statistics, supports file import/export, and allows keyword search within code.

![Description of Screenshot](./Screenshots/startup.PNG)

---

## ✨ Features

### 🔹 Load Code Blocks

![Description of Screenshot](./Screenshots/imported_files.PNG)

* You can **manually paste code** into the input fields or **import code from files** (`.cs`, `.txt`, `.java`, `.py`, etc.).
* The name of the imported file is displayed above each code block for clarity.

### 🔹 Code Comparison

![Description of Screenshot](./Screenshots/compared_files.PNG)

* The app compares the two blocks line by line and aligns them side-by-side.
* It detects and displays:

  * **Inserted lines** (green),
  * **Deleted lines** (red),
  * **Modified lines** (orange),
  * and unchanged lines.

### 🔹 Visual Highlighting

* Differences are color-coded:

  * 🟥 Deleted lines – `LightCoral`
  * 🟩 Inserted lines – `LightGreen`
  * 🟧 Modified lines – `PeachPuff`
* You can quickly spot where changes occurred.

### 🔹 Search

![Description of Screenshot](./Screenshots/search.PNG)

* You can search for any term or keyword.
* Matching lines are underlined to improve visibility in large files.

### 🔹 Statistics Summary

![Description of Screenshot](./Screenshots/statistics.PNG)

* The app shows the number of:

  * Same lines
  * Inserted lines
  * Deleted lines
  * Modified lines

### 🔹 Export Results

![Description of Screenshot](./Screenshots/export.PNG)

* After comparing, you can **export the comparison results** to a `.txt` file.
* The exported file contains:

  * Summary statistics
  * A full list of differences with labels

---

## 🛠️ Technologies Used

* **WPF** (Windows Presentation Foundation)
* **MVVM** architecture
* **DiffPlex** for code comparison logic
* **MaterialDesignInXAML** for modern UI components

