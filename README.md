# API Active Directory

### 👨🏻‍💻 About
This project aims to facilitate authentication, group management and users using Active Directory (Windows Server). 
Because of the libraries used for integration with Active Directory, this project runs only on windows machines that are inserted in the domain of the company.
Fell free to change and colaborate with this project.

### 🚀 Running the project
- Insert the necessary environment variables (APIAD_USER as json)
  - The json structure is like { "admin": { "user": "", "password": "" }, "read": { "user": "", "password": "" }}
  - Fell free to change this call for environment variable for a config json file, remember to change this in file Settings.json
- For the secret, insert a environment variable call APIAD_SECRET and any secure value
