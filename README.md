# PatchFM

PatchFM is a tool that aims at making your LastFM scrobbles more accurate.

# Inaccurate Scrobble

Here's the current inaccuracies that PatchFM tries to fix.

1. Scrobble having an empty album ![empty-album](./docs/img/empty-album.png)
2. Different album name for the same song ![different-album](./docs/img/different-album.png)

If you encounter an inaccuracy not taken into consideration by this tool, feel free to open an issue in this repo.

# How to execute it

Download the appropriate exectuable on the [release](https://github.com/pnolin/PatchFm/releases) page.

Run the following command `PatchFm[.exe] --LastFmUsername={Username} --StartDate={yyyy-mm-dd}`

Where `{Username}` is replaced by your LastFm Username and `{yyyy-mm-dd}` is replaced by the date you want the tool to start fetching Scrobbles from.
If you don't provide a value for the `StartDate` parameter then the Start Date will bet to the date time you are running the tool minus 24 hours.

# Fixing the inaccuracies

Since the LastFm API doesn't allow editing nor deleting Scrobbles, the tool can't do that.
Instead, the tool will create a file under the ".\patches\[date]-patch-fm.txt" file. 
Where [date] is replaced by the date at which the tool is ran in the format yyyy-mm-dd.

The patches folder is created in the same folder where the executable is.

The file contains a list of suggested transformations that when applied will make your LastFm statistics more accurate.
These transformations can be applied manually in LastFm since it allows editting scrobbles.

# Contributing

The only non-technical prerequisite to build and run the application is to have an LastFm Api Key and Secret.
You can create that [here](https://www.last.fm/api/account/create).

You can either set your Key and Secret in the appsettings.json (without pushing the Keys) or by using .netcore [Secret Manager](https://docs.microsoft.com/en-us/aspnet/core/security/app-secrets?view=aspnetcore-3.1&tabs=windows).
The Username and Start Date can be set in the appsettings.json or via the command line.

Pull Requests are welcome!