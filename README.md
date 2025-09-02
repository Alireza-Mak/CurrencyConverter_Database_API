## 👋 Hi, I'm Alireza!


## 👨‍💻 About Me
I'm a full-stack developer with 5 years of experience, passionate about game development and creative coding.

## 🌐 Portfolio:
[alirezamak.com](https://alirezamak.com)


## 🚀 About the Project: 
**Currency Convertor — Static Data**

A WPF desktop application that converts between currencies using **live exchange rates** fetched from an external API and cached in a **local SQL Server database**. The UI is built with XAML and styled with modern controls, while the backend handles API calls, JSON deserialization, and database updates. This project demonstrates full integration of **networking, database, and UI layers** in a lightweight desktop app.

## ✨ Features

**1.** Fetches live exchange rates from an external API with `HttpClient`  

**2.** Deserializes JSON responses into strongly typed objects using `Newtonsoft.Json`  

**3.** Stores rates in a local SQL Server `.mdf` database (`Currency_Master` table)  

**4.** Supports conversion between USD, CAD, EUR, GBP, QAR (easily extendable)  

**5.** Automatic number formatting (grouping separators + decimals) for readability  

**6.** Validation for empty inputs and duplicate currency selections  

**7.** Clear and Swap actions for smooth usability  

**8.** Error handling for failed API calls or database issues  


## 📸 Game Screenshot

![Currency Convertor-Static Data Screenshot 1](https://alirezamak.com/wp-content/uploads/Currency-Convertor-DB-and-API-1.png)
![Currency Convertor-Static Data Screenshot 2](https://alirezamak.com/wp-content/uploads/Currency-Convertor-DB-and-API-2.png)
![Currency Convertor-Static Data Screenshot 2](https://alirezamak.com/wp-content/uploads/Currency-Convertor-DB-and-API-3.png)
![Currency Convertor-Static Data Screenshot 2](https://alirezamak.com/wp-content/uploads/Currency-Convertor-DB-and-API-4.png)
![Currency Convertor-Static Data Screenshot 3](https://alirezamak.com/wp-content/uploads/Currency-Convertor-DB-and-API-5.png)


## 🛠 Skills

- **C# and WPF (XAML):** desktop UI design and logic  
- **API Integration:** `HttpClient` with async/await and timeouts  
- **JSON Processing:** deserialization with `Newtonsoft.Json`  
- **Database Management:** SQL Server LocalDB, `SqlConnection`, `SqlCommand`  
- **Input Handling:** validation, number formatting, and user-friendly controls  
- **Error Handling:** exception catching with user notifications  


## 📁 Folder Structure

```
Currency_Convertor_Static_Data/
│── App.xaml # Application resources and startup settings
│── App.xaml.cs # Application class logic
│── MainWindow.xaml # UI layout (WPF XAML)
│── MainWindow.xaml.cs # Code-behind with event handling and logic
│── Database/ # Local .mdf database
│── Models/ # Root + Rates classes for JSON
│── Images/ # UI icons and assets
│── Properties/ # Project metadata and settings
│── bin/ # Compiled binaries
│── obj/ # Build objects and temporary files
└── README.md
```


## 🛠️ Build & Run Instructions

**Using Visual Studio 2022 (Windows)**
```
1. Clone the repository: git clone https://github.com/Alireza-Mak/Currency-Converter-API-DB.git
2. Open the project in Visual Studio  
3. Build the solution  
4. Run the application  

⚠️ **Note 1:** Install **FontAwesome** and **Newtonsoft.Json** packages before running to ensure icons display correctly.  
⚠️ **Note 2:** Confirm SQL Server LocalDB is available and remove .ldf file if issues arise.
⚠️ **Note 3:** Internet connection required for fetching rates.
```

## ✉️ Support

For any questions or feedback, feel free to reach out:  
📧 **info@alirezamak.com**


## 👤 Author

**Alireza Mak**  
GitHub: [@Alireza-Mak](https://github.com/Alireza-Mak)


## 🔗 Links
[![Portfolio](https://img.shields.io/badge/My_Portfolio-000?style=for-the-badge&logo=ko-fi&logoColor=white)](https://alirezamak.com/)
[![LinkedIn](https://img.shields.io/badge/LinkedIn-0A66C2?style=for-the-badge&logo=linkedin&logoColor=white)](https://www.linkedin.com/in/alireza-mak/)
[![Email](https://img.shields.io/badge/Email-D14836?style=for-the-badge&logo=gmail&logoColor=white)](mailto:info@alirezamak.com)


## Happy to Receive Your Feedback! 😊
- Enjoy using the app and feel free to reach out for support or suggestions! 🎉