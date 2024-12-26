
# ColorPalette

ColorPalette is a .NET Core Web API designed to extract dominant colors from uploaded images.

### Features:

* **Image Upload:** Allows users to upload images for analysis.
* **Color Extraction:** Extracts a specified number of dominant colors from the image.
* **Color Code Generation:** Generates HEX color codes.

### Technologies Used:

* **.NET Core:** The robust framework used for building the Web API.
* **ImageSharp:** A high-performance, cross-platform image processing library.
* **C#:** The primary programming language for the project.

### Getting Started:

1. **Clone the repository:**
    ```bash
    gh repo clone farukcakal/ColorPalette
    ```

2. **Restore dependencies:**
    ```bash
    dotnet restore
    ```

3. **Run the application:**
    ```bash
    dotnet run
    ```

### API Endpoints:

**POST /api/palette/extractcolors**

**Request Body:**
```JSON
{
    "file": "string binary image data",
    "colorCount": 5
}
```

**Response:**
```JSON
{
    "message": "Fotoğraf başarıyla yüklendi ve renkler çıkarıldı.",
    "colors": [
        "#FF0000",
        "#00FF00",
        "#0000FF",
        // ...
    ]
}
```
