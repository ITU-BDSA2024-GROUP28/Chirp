---
title: _Chirp!_ Project Report
subtitle: ITU BDSA 2024 Group 28
author:
- "Amira Maria Ayoub <aayo@itu.dk>"
- "Emma Andrea Ravn Krause-Kjær <ekra@itu.dk>"
- "Josefine Kamp Nielsen <kajn@itu.dk>"
- "Stine Helena Hallberg Sørensen <sths@itu.dk>"
- "Um-Kulsum Abusheva <umab@itu.dk>"
numbersections: true
---

# Design and Architecture of _Chirp!_

## Domain model

![](./images/DomainModel.drawio.png)

The Domain Model for the Chirp application is implemented in the Chirp.Core package, which is the innermost layer in our Onion Model. This model consists of two main classes/entities: Author and Cheep, which define the essential components, core behavior and structure of the application. 

Author represents a user and extends from the IdentityUser class from Asp.Net.Core Identity, allowing functionality such as authenticating a user. As seen above in the diagram, an Author automatically inherits an Id (int), an Email (string) and a username (string). Furthermore, an Author has a relation to Cheep by storing a Collection of cheeps the user has written. This ensures that every Cheep is written by only one Author, but an Author can write many Cheeps, making it a one-to-many relationship. Finally, the Author class stores two Lists, which contain the other Authors which the user is either following or followed by.

The Cheep object represents the structure for individual posts created by an authorizeded user. It contains a unique identifier CheepId (int), a foreign key AuthorId (int) and an Author of type Author, associated with an existing user. It also contains Text (string), which is the content of the post with a maximum length of 160 characters. Lastly, it contains a Timestamp (DateTime), which is the registered time and date of when the cheep was posted.

## Architecture — In the small

## Architecture of deployed application

## User activities

## Sequence of functionality/calls trough _Chirp!_

# Process

## Build, test, release, and deployment
Our program is automatically built, tested and run through the following three Github Actions Workflows

### Build and Test Workflow
![](./diagrams/workflow1.png)

This workflow shows how we automatically build and then test our program on all branches whenever we push our commits or accept a pull request. This helps keep us on track with testing.

### Release Workflow
![](./diagrams/workflow2.png)

This workflow shows how we automatically create releases for the three major operating systems. These releases allow a user to download and run the program locally from their own computer. The workflow is triggered by the creation and push of a new tag of the form “v*.*.*” (RegEx: the asterisks represents any number/character). The entire project is compressed into single file executables for each OS, and then further compressed together with the static files into zip files. These, together with the source code in zip and tar.gz files are released to GitHub under the version number.

The details of building the program have been left out here, as they are shown in the build and test workflow.

### Deployment Workflow
![](./diagrams/workflow3.png)

This workflow illustrates how we deploy our program to Azure. This workflow is almost entirely built by Azure, we have simply modified it to suit the specifics of our program. The workflow is triggered by either a push or pull to the primary branch.

THe details of building and testing the program have also been left out here, as they are shown in the build and test workflow.

## Team work
![](./images/project_board.png)

Our project board columns have been adjusted throughout this course, due to our needs varying from week to week. In the beginning of the course we were still learning to structure our time correctly, so we had columns for previous weeks that included issues we had not managed to finish before the beginning of a new week. However, with a bit of extra effort we caught up and for the last half of the course we have only had work for the current week to complete. This can be seen in the image above. 

As can also be seen above, some issues have not yet been completed. This is due to us constantly improving our project these last few days, so occassionally new warnings pop up, and tests need to be adjusted. These issues have therefore been ongoing for longer periods of time, and have been moved back and forth between the in progress and completed columns. There are also some issues on the board which reflect the status of our report at the moment of us writing this section. 

![](./diagrams/groupworkflowblue.png)

This is how our group tackled the weekly project work. As can be seen from the diagram, the flow in the blue box was used repeatedly throughout the week, as this is how we structured our work in smaller groups when working directly on the project.

We followed the standard pair programming strategies well throughout the weeks, and enjoyed how efficient we found this to be. As can also be seen in many of our initial commits, we did sometimes spend the days working all of us together on the project work. This was often due to certain tasks needing to be performed sequentially, otherwise the project would not be cohesive. Additionally, we enjoyed the productive discussions that sprung from this team-working style. 

We also enjoyed showing eachother our work by conducting scrum-style code run-throughs when meeting up  all together, as this helped us all stay up to date on the code, even the parts we had not written ourselves. This also allowed for inputs on how to improve certain parts of the code in terms of efficiency, better readability or to follow the correct architectural design models.

## How to make _Chirp!_ work locally

### Using a release

#### For Windows

1. Go to our [GitHub repo](https://github.com/ITU-BDSA2024-GROUP28/Chirp).
2. Click on the newest release of Chirp! found under Releases
3. Download the zip file for Windows OS
4. After the downloaded completes, right click the zip file and extract the files.
5. After the files have beem extracted, run the "Chirp.Web.exe"
6. The terminal should now show a lot of text. Find the sentence "Now listening on: http​﻿://localhost:XXXX" and note the port number.
7. Click the link or type this URL into your web browser and press enter to open the Chirp! web app.
8. You should be able to see the "Public timeline" with several cheeps displayed.
9. Press the reigster button in the navigation bar and register using Github or use your private email and username, and create a password.
10. You should now be able to freely explore Chirp!

#### For MacOS X

1. Go to our [GitHub repo](https://github.com/ITU-BDSA2024-GROUP28/Chirp). 
2. Click on the newest release of Chirp! found under Releases.
3. Download the zip file for MacOS.
4. After the downloaded completes, double click the zip file and extract the folder.
5. After the folder have beem extracted, right click the folder and select "New terminal at folder".
6. Type the following command into the terminal:
```
sudo ./Chirp.Web
```
7. Enter the password for your device when prompted, and press enter.
8. If you receive the warning "'Chirp.Web' cannot be opened because the developer cannot be verified" follow these steps to bypass it.

   a. Close the warning by pressing "Cancel".

   b. Go to "System Preferences" on your Mac.

   c. Click on "Privacy and Security".

   d. Scroll to "Security".

   e. You should see a message saying "Chirp.Web was blocked from opening because it is not from an identified developer."

   f. Click on the "Allow Anyway" next to it.

   g. Go back to the terminal and type the same command into the terminal:
   ```
   sudo ./Chirp.Web
   ```
   
   h. Enter the password for your device when prompted, and press enter.
   
9. If a new warning shows up saying: "macOS cannot verify the developer of “Chirp.Web”. Are you sure you want to open it?" Press "Open".
10. The terminal should now show a lot of text. Find the sentence "Now listening on: http​﻿://localhost:XXXX" and note the port number.
11. Click the link or type this URL into your web browser and press enter to open the Chirp! web app.
12. You should be able to see the "Public timeline" with several cheeps displayed.
13. Press the reigster button in the navigation bar and register using Github or use your private email and username, and create a password.
14. You should now be able to freely explore Chirp!

### Using github cloning

In the **Terminal**:

1. Open a new terminal/command prompt in the folder you would like to contain Chirp, enter:

```
git clone https://github.com/ITU-BDSA23-GROUP16/Chirp.git
```

2. After the cloning process has completed, navigate into the project directory using:

```
cd Chirp
```

3. You may need to set the correct clientId and clientSecret before running. Use the following commands:

```
dotnet user-secrets init --project src/Chirp.Infrastructure
```

```
dotnet user-secrets set "authentication_github_clientId" "<clientId" --project src/Chirp.Infrastructure
```

```
dotnet user-secrets set "authentication_github_clientSecret" "<clientSecret>" --project src/Chirp.Infrastructure
```

4. Run the project by entering:

```
dotnet run --project src/Chirp.Web.
```

## How to run test suite locally

# Ethics

## License
For this project our group chose to use the MIT license. Due to most of the packages we use being licensed under MIT, this was the most logical choice. Moreover, we value the simplicity of the license, as we are not experienced in using licenses, and as developers we appreciate the flexibility and freedom this allows. The license gives any person “without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software” under the condition that the copyright notice and the permission of MIT license is included in all copies. [source]

[source] our license is chosen from [choosealicense.com](https://choosealicense.com/licenses/mit/), see file [LICENSE.md](https://github.com/ITU-BDSA2024-GROUP28/Chirp/blob/Ethics/LICENSE.MD)

## LLMs, ChatGPT, CoPilot, and others
We have used the LLM ‘ChatGPT’ in a few cases. We have made sure to mention this in our commits whenever we have done so. In all cases the purpose was to give a new perspective on a problem that had us stumped. However, it has almost always been more helpful and beneficial to ask classmates or TAs, we only resorted to the LLM when they were not available to assist. Whenever we did ask the LLM for help, it would only speed up our work approximately 50% of the time. The remaining 50% of the responses it gave to our prompts were mostly, if not entirely, useless.

