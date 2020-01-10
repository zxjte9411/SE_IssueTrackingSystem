# Issue Tracking System Team#14

## 組員名單
- 王方顯 105590011
- 林彥儒 105590017
- 涂家瑋 105590025
- 楊永健 105590045
- 卓旭嘉 105590046
- 江俊廷 105820006
## 目錄結構
```
- demoVideo
    | - 一般使用者 (4)
    | - 管理員 (2)
- doc
    | - class diagram(3)
    | - sequence diagram(10)
    | - system block diagram(6)
    | - use case diagram(6)
    | - 簡報(2)
    | - PEP.pdf
    | - SRS.pdf
    | - SDD.pdf
    | - STD.pdf
    | - 分工結構圖.png
- project
    | - issue-tracking-system-frontend-master
- README.md
```
## Development Back End Server

### Reqirement
- SQL Server Management Studio
- .Net Core SDK 2.2 ([Link](https://dotnet.microsoft.com/download/dotnet-core/2.2))
    - Verification
        ```
        dotnet --version
        ```
        should display
        ```
        2.2.xxx
        ```
### Setup Database
Open SSMS, execute all the sql file below .\project\issue-tracking-system-backend-master\DataBase

### Connection string
Modify the file appsettings.json at .\project\issue-tracking-system-backend-master\IssueTrackingSystemApi\IssueTrackingSystemApi

Change the credential of section "DBConnection", showing the format below:
```
"DBConnection": "Data Source={DATA_SOURCE};Initial Catalog=ITS;User ID={ID};Password={PASSWORD}"
```

### Run the server
- Open cmd.exe
- cd to repository folder
- cd to location of api
    ```
    cd .\project\issue-tracking-system-backend-master\IssueTrackingSystemApi\IssueTrackingSystemApi
    ```
- run the server
    ```
    dotnet run
    ```
The console should contain:
```
Now listening on: http://localhost:5000
Application started. Press Ctrl+C to shut down.
```


## Development Front End Server

### 安裝步驟
1. 下載並安裝 [Node.js](https://nodejs.org/dist/v12.14.1/node-v12.14.1-x64.msi)
2. 下載專案 [Github](https://github.com/oscarada87/issue-tracking-system-frontend.git)
3. 進入到專案資料夾後開啟 PowerShell 或 CMD，按下列步驟進行部屬
    ```
    npm install
    ```
    ```
    npm run serve
    ```
4. 在瀏覽器輸入此連結即可使用本系統： 
    ```
    http://localhost:8080/
    ```
### 預設可用帳號
- 預設系統管理員帳號：Admin
- 預設系統管理員密碼：admin
- 預設系一般使用者帳號：Test01
- 預設系一般使用者密碼：test
### 參考
- 後端 Github link：https://github.com/YangKelvin/IssueTrackingSystem.git

- 前端 Github link：https://github.com/oscarada87/issue-tracking-system-frontend.git

    > 都有詳細 commit 紀錄
