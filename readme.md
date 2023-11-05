# IMDB API (Free) #
The free **IMDb API** is your cinematic information hub. Access movie, TV series, and cast details in JSON format, including movie specs, images, posters, trailers, ratings and more with ease. Dive into the world of cinema effortlessly.

# Contacts #
Help us improve the library. Write to us on our [gitlab repository](https://github.com/cata76/IMDb) or [by email](mailto:cata76@virgilio.it) writing to cata76@virgilio.it
Request your needs or ideas to improve this product.
Please report any anomalies or errors you encounter during use

## Usage ##
Search by title, celebrities, companies and keywords within the IMDb archive.
```csharp
{
    var myIMDb = new IMDb.IMDb();
    IMDb.Results myResults = myIMDb.search("Interstellar");
    IMDb.IMDb.Title myTitle = myIMDb.title(myResults.titles(0).id);
}
```
## Data structures ##
# IMDb.Results #
Below is the IMDb.Results data structure populated by searching for the title 'Interstellar'
```json
{
  "searchTerm": "interstellar",
  "searchType": "",
  "titles": [
    {
      "id": "tt0816692",
      "titleNameText": "Interstellar",
      "titleReleaseText": "2014",
      "titleTypeText": "",
      "imageType": "movie",
      "topCredits": [
        "Matthew McConaughey",
        "Anne Hathaway"
      ],
      "poster": {
        "id": "",
        "url": "https://m.media-amazon.com/images/M/MV5BMjI3MTMyMjgtM2U4Yi00NDE5LTg1NzgtZWMzM2YwM2Y3ZjU2XkEyXkFqcGdeQXVyMTYzMDM0NTU@._V1_.jpg",
        "width": "2391",
        "height": "3543",
        "caption": "Matthew McConaughey in Interstellar (2014)"
      },
      "series": {
        "id": "",
        "name": "",
        "release": "",
        "type": "",
        "season": "",
        "episode": ""
      }
    },
    {
      "id": "tt10728262",
      "titleNameText": "Interstellar",
      "titleReleaseText": "2018",
      "titleTypeText": "Short",
      "imageType": "short",
      "topCredits": [
        "Kitty Paitazoglou"
      ],
      "poster": {
        "id": "",
        "url": "https://m.media-amazon.com/images/M/MV5BMTNmZjA1NWYtM2RhYi00YWY2LWJkNDQtNWY5MGE5N2I4ZWIwXkEyXkFqcGdeQXVyNzg5OTk2OA@@._V1_.jpg",
        "width": "690",
        "height": "316",
        "caption": "Interstellar (2018)"
      },
      "series": {
        "id": "",
        "name": "",
        "release": "",
        "type": "",
        "season": "",
        "episode": ""
      }
    },
    {
      "id": "tt26699362",
      "titleNameText": "Interstellar Ella",
      "titleReleaseText": "2022– ",
      "titleTypeText": "TV Series",
      "imageType": "tvSeries",
      "topCredits": [
        "Ava Augustin",
        "Felicia Shulman"
      ],
      "poster": {
        "id": "",
        "url": "https://m.media-amazon.com/images/M/MV5BMTEzNmQzODctMjZhNS00MGU2LThhYzctMTZiNDdjNDFkZDlkXkEyXkFqcGdeQXVyNjcwODQ2Nzc@._V1_.jpg",
        "width": "2048",
        "height": "2926",
        "caption": "Interstellar Ella (2022)"
      },
      "series": {
        "id": "",
        "name": "",
        "release": "",
        "type": "",
        "season": "",
        "episode": ""
      }
    },
    {
      "id": "tt4415360",
      "titleNameText": "The Science of Interstellar",
      "titleReleaseText": "2015",
      "titleTypeText": "",
      "imageType": "movie",
      "topCredits": [
        "Matthew McConaughey",
        "Kip Thorne"
      ],
      "poster": {
        "id": "",
        "url": "https://m.media-amazon.com/images/M/MV5BMDFhNzU4MTMtYzZmNS00ZDEzLTg2MjQtYmUzZDA1ZWU4OTkzXkEyXkFqcGdeQXVyNDQ2MTMzODA@._V1_.jpg",
        "width": "690",
        "height": "1024",
        "caption": "The Science of Interstellar (2015)"
      },
      "series": {
        "id": "",
        "name": "",
        "release": "",
        "type": "",
        "season": "",
        "episode": ""
      }
    },
    {
      "id": "tt5056352",
      "titleNameText": "Interstellar Civil War",
      "titleReleaseText": "2017",
      "titleTypeText": "",
      "imageType": "movie",
      "topCredits": [
        "Kenzie Phillips",
        "Brad Thornton"
      ],
      "poster": {
        "id": "",
        "url": "https://m.media-amazon.com/images/M/MV5BMDJmYjQ4YmEtOTkzMS00MzQ3LWExNmUtMzgzZWQxMWJkZjk2XkEyXkFqcGdeQXVyMzM1MjQzNTk@._V1_.jpg",
        "width": "960",
        "height": "540",
        "caption": "Brad Thornton, Ellie Church, and Mayling Ng in Interstellar Civil War (2017)"
      },
      "series": {
        "id": "",
        "name": "",
        "release": "",
        "type": "",
        "season": "",
        "episode": ""
      }
    }
  ],
  "celebs": [
    {
      "id": "nm11625104",
      "displayNameText": "Atesh Sakarya",
      "knownForTitleText": "Object Havoc",
      "knownForTitleYear": "2014–2015",
      "knownForJobCategory": "Animation Department",
      "avatar": {
        "id": "",
        "url": "",
        "width": "0",
        "height": "0",
        "caption": ""
      },
      "akaName": ""
    },
    {
      "id": "nm11723055",
      "displayNameText": "Insterstella Carrot",
      "knownForTitleText": "Object Horror",
      "knownForTitleYear": "2020– ",
      "knownForJobCategory": "Animation Department",
      "avatar": {
        "id": "",
        "url": "",
        "width": "0",
        "height": "0",
        "caption": ""
      },
      "akaName": ""
    },
    {
      "id": "nm2273774",
      "displayNameText": "Lynne Wintersteller",
      "knownForTitleText": "Jessica Jones",
      "knownForTitleYear": "2015–2019",
      "knownForJobCategory": "Actress",
      "avatar": {
        "id": "",
        "url": "https://m.media-amazon.com/images/M/MV5BMTUyNjQ3Nzk1Ml5BMl5BanBnXkFtZTgwMDEyMzI0MDI@._V1_.jpg",
        "width": "2400",
        "height": "3600",
        "caption": "Lynne Wintersteller"
      },
      "akaName": ""
    },
    {
      "id": "nm0144657",
      "displayNameText": "Dan Castellaneta",
      "knownForTitleText": "I Simpson",
      "knownForTitleYear": "1989– ",
      "knownForJobCategory": "Actor",
      "avatar": {
        "id": "",
        "url": "https://m.media-amazon.com/images/M/MV5BMjAyNDY5NDEwOV5BMl5BanBnXkFtZTcwMjY0ODYyMQ@@._V1_.jpg",
        "width": "224",
        "height": "314",
        "caption": "Dan Castellaneta"
      },
      "akaName": ""
    },
    {
      "id": "nm3403837",
      "displayNameText": "Stella Interlenghi",
      "knownForTitleText": "Top Crack",
      "knownForTitleYear": "1967",
      "knownForJobCategory": "Actress",
      "avatar": {
        "id": "",
        "url": "",
        "width": "0",
        "height": "0",
        "caption": ""
      },
      "akaName": ""
    }
  ],
  "companies": [],
  "keywords": [],
  "errorMessage": ""
}
```
# IMDb.Title #
Below is the IMDb.Title data structure populated by searching movie id 'Interstellar'
```json
{
  "searchTerm": "interstellar",
  "searchType": "",
  "titles": [
    {
      "id": "tt0816692",
      "titleNameText": "Interstellar",
      "titleReleaseText": "2014",
      "titleTypeText": "",
      "imageType": "movie",
      "topCredits": [
        "Matthew McConaughey",
        "Anne Hathaway"
      ],
      "poster": {
        "id": "",
        "url": "https://m.media-amazon.com/images/M/MV5BMjI3MTMyMjgtM2U4Yi00NDE5LTg1NzgtZWMzM2YwM2Y3ZjU2XkEyXkFqcGdeQXVyMTYzMDM0NTU@._V1_.jpg",
        "width": "2391",
        "height": "3543",
        "caption": "Matthew McConaughey in Interstellar (2014)"
      },
      "series": {
        "id": "",
        "name": "",
        "release": "",
        "type": "",
        "season": "",
        "episode": ""
      }
    },
    {
      "id": "tt10728262",
      "titleNameText": "Interstellar",
      "titleReleaseText": "2018",
      "titleTypeText": "Short",
      "imageType": "short",
      "topCredits": [
        "Kitty Paitazoglou"
      ],
      "poster": {
        "id": "",
        "url": "https://m.media-amazon.com/images/M/MV5BMTNmZjA1NWYtM2RhYi00YWY2LWJkNDQtNWY5MGE5N2I4ZWIwXkEyXkFqcGdeQXVyNzg5OTk2OA@@._V1_.jpg",
        "width": "690",
        "height": "316",
        "caption": "Interstellar (2018)"
      },
      "series": {
        "id": "",
        "name": "",
        "release": "",
        "type": "",
        "season": "",
        "episode": ""
      }
    },
    {
      "id": "tt26699362",
      "titleNameText": "Interstellar Ella",
      "titleReleaseText": "2022– ",
      "titleTypeText": "TV Series",
      "imageType": "tvSeries",
      "topCredits": [
        "Ava Augustin",
        "Felicia Shulman"
      ],
      "poster": {
        "id": "",
        "url": "https://m.media-amazon.com/images/M/MV5BMTEzNmQzODctMjZhNS00MGU2LThhYzctMTZiNDdjNDFkZDlkXkEyXkFqcGdeQXVyNjcwODQ2Nzc@._V1_.jpg",
        "width": "2048",
        "height": "2926",
        "caption": "Interstellar Ella (2022)"
      },
      "series": {
        "id": "",
        "name": "",
        "release": "",
        "type": "",
        "season": "",
        "episode": ""
      }
    },
    {
      "id": "tt4415360",
      "titleNameText": "The Science of Interstellar",
      "titleReleaseText": "2015",
      "titleTypeText": "",
      "imageType": "movie",
      "topCredits": [
        "Matthew McConaughey",
        "Kip Thorne"
      ],
      "poster": {
        "id": "",
        "url": "https://m.media-amazon.com/images/M/MV5BMDFhNzU4MTMtYzZmNS00ZDEzLTg2MjQtYmUzZDA1ZWU4OTkzXkEyXkFqcGdeQXVyNDQ2MTMzODA@._V1_.jpg",
        "width": "690",
        "height": "1024",
        "caption": "The Science of Interstellar (2015)"
      },
      "series": {
        "id": "",
        "name": "",
        "release": "",
        "type": "",
        "season": "",
        "episode": ""
      }
    },
    {
      "id": "tt5056352",
      "titleNameText": "Interstellar Civil War",
      "titleReleaseText": "2017",
      "titleTypeText": "",
      "imageType": "movie",
      "topCredits": [
        "Kenzie Phillips",
        "Brad Thornton"
      ],
      "poster": {
        "id": "",
        "url": "https://m.media-amazon.com/images/M/MV5BMDJmYjQ4YmEtOTkzMS00MzQ3LWExNmUtMzgzZWQxMWJkZjk2XkEyXkFqcGdeQXVyMzM1MjQzNTk@._V1_.jpg",
        "width": "960",
        "height": "540",
        "caption": "Brad Thornton, Ellie Church, and Mayling Ng in Interstellar Civil War (2017)"
      },
      "series": {
        "id": "",
        "name": "",
        "release": "",
        "type": "",
        "season": "",
        "episode": ""
      }
    }
  ],
  "celebs": [
    {
      "id": "nm11625104",
      "displayNameText": "Atesh Sakarya",
      "knownForTitleText": "Object Havoc",
      "knownForTitleYear": "2014–2015",
      "knownForJobCategory": "Animation Department",
      "avatar": {
        "id": "",
        "url": "",
        "width": "0",
        "height": "0",
        "caption": ""
      },
      "akaName": ""
    },
    {
      "id": "nm11723055",
      "displayNameText": "Insterstella Carrot",
      "knownForTitleText": "Object Horror",
      "knownForTitleYear": "2020– ",
      "knownForJobCategory": "Animation Department",
      "avatar": {
        "id": "",
        "url": "",
        "width": "0",
        "height": "0",
        "caption": ""
      },
      "akaName": ""
    },
    {
      "id": "nm2273774",
      "displayNameText": "Lynne Wintersteller",
      "knownForTitleText": "Jessica Jones",
      "knownForTitleYear": "2015–2019",
      "knownForJobCategory": "Actress",
      "avatar": {
        "id": "",
        "url": "https://m.media-amazon.com/images/M/MV5BMTUyNjQ3Nzk1Ml5BMl5BanBnXkFtZTgwMDEyMzI0MDI@._V1_.jpg",
        "width": "2400",
        "height": "3600",
        "caption": "Lynne Wintersteller"
      },
      "akaName": ""
    },
    {
      "id": "nm0144657",
      "displayNameText": "Dan Castellaneta",
      "knownForTitleText": "I Simpson",
      "knownForTitleYear": "1989– ",
      "knownForJobCategory": "Actor",
      "avatar": {
        "id": "",
        "url": "https://m.media-amazon.com/images/M/MV5BMjAyNDY5NDEwOV5BMl5BanBnXkFtZTcwMjY0ODYyMQ@@._V1_.jpg",
        "width": "224",
        "height": "314",
        "caption": "Dan Castellaneta"
      },
      "akaName": ""
    },
    {
      "id": "nm3403837",
      "displayNameText": "Stella Interlenghi",
      "knownForTitleText": "Top Crack",
      "knownForTitleYear": "1967",
      "knownForJobCategory": "Actress",
      "avatar": {
        "id": "",
        "url": "",
        "width": "0",
        "height": "0",
        "caption": ""
      },
      "akaName": ""
    }
  ],
  "companies": [],
  "keywords": [],
  "errorMessage": ""
}
```
