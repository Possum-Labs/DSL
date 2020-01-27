# Setup Instructions

1. Run `brew install mono-libgdiplus`
2. Install VS Code, then install the C# extension for VS Code
3. Install .NET core SDK 3.1.x from https://dotnet.microsoft.com/download/dotnet-core/3.1
4. Install docker desktop and start docker (if using linux this step is considerably more involved, see the docker website for detailed instructions)
5. Cd `/development-setup/` && run ` docker-compose up --scale node-chrome=3 -d`
6. Go to localhost:4444/grid/console in your browser. You should see three web drivers.