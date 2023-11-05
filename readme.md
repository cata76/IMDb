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
  "id": "tt0816692",
  "title": "Interstellar",
  "originalTitle": "Interstellar",
  "type": "Movie",
  "year": "2014",
  "image": {
    "id": "rm2726982656",
    "url": "https://m.media-amazon.com/images/M/MV5BMjI3MTMyMjgtM2U4Yi00NDE5LTg1NzgtZWMzM2YwM2Y3ZjU2XkEyXkFqcGdeQXVyMTYzMDM0NTU@._V1_.jpg",
    "width": "2391",
    "height": "3543",
    "caption": "Matthew McConaughey in Interstellar (2014)"
  },
  "releaseDate": "6/11/2014",
  "runtime": {
    "seconds": "10140",
    "plainText": "2h 49m"
  },
  "plot": "When Earth becomes uninhabitable in the future, a farmer and ex-NASA pilot, Joseph Cooper, is tasked to pilot a spacecraft, along with a team of researchers, to find a new planet for humans.",
  "awards": {
    "rank": "22",
    "wins": "44",
    "eventId": "ev0000003",
    "awardId": "aw0000016",
    "awardText": "Oscar",
    "awardWins": "1",
    "awardNominations": "",
    "nomination": "148"
  },
  "directors": [
    {
      "id": "nm0634240",
      "name": "Christopher Nolan",
      "image": {
        "id": "",
        "url": "",
        "width": "0",
        "height": "0",
        "caption": ""
      },
      "category": "Director",
      "characters": []
    }
  ],
  "writers": [
    {
      "id": "nm0634240",
      "name": "Christopher Nolan",
      "image": {
        "id": "",
        "url": "",
        "width": "0",
        "height": "0",
        "caption": ""
      },
      "category": "Writers",
      "characters": []
    }
  ],
  "actors": [
    {
      "id": "nm0000190",
      "name": "Matthew McConaughey",
      "image": {
        "id": "",
        "url": "https://m.media-amazon.com/images/M/MV5BMTg0MDc3ODUwOV5BMl5BanBnXkFtZTcwMTk2NjY4Nw@@._V1_.jpg",
        "width": "1256",
        "height": "2048",
        "caption": ""
      },
      "category": "actor",
      "characters": [
        "Cooper"
      ]
    },
    {
      "id": "nm0004266",
      "name": "Anne Hathaway",
      "image": {
        "id": "",
        "url": "https://m.media-amazon.com/images/M/MV5BMTRhNzQ3NGMtZmQ1Mi00ZTViLTk3OTgtOTk0YzE2YTgwMmFjXkEyXkFqcGdeQXVyNzg5MzIyOA@@._V1_.jpg",
        "width": "2417",
        "height": "3000",
        "caption": ""
      },
      "category": "actress",
      "characters": [
        "Brand"
      ]
    },
    {
      "id": "nm1567113",
      "name": "Jessica Chastain",
      "image": {
        "id": "",
        "url": "https://m.media-amazon.com/images/M/MV5BMTU1MDM5NjczOF5BMl5BanBnXkFtZTcwOTY2MDE4OA@@._V1_.jpg",
        "width": "1268",
        "height": "1884",
        "caption": ""
      },
      "category": "actress",
      "characters": [
        "Murph"
      ]
    },
    {
      "id": "nm3237775",
      "name": "Mackenzie Foy",
      "image": {
        "id": "",
        "url": "https://m.media-amazon.com/images/M/MV5BYzg2YjllNDUtNGMwNy00NzYzLTlkMWQtMTc3ZWI4NDI5N2U2XkEyXkFqcGdeQXVyMDM2NDM2MQ@@._V1_.jpg",
        "width": "2632",
        "height": "3948",
        "caption": ""
      },
      "category": "actress",
      "characters": [
        "Murph (10 Yrs.)"
      ]
    },
    {
      "id": "nm0000995",
      "name": "Ellen Burstyn",
      "image": {
        "id": "",
        "url": "https://m.media-amazon.com/images/M/MV5BMTU4MjYxMDc3MF5BMl5BanBnXkFtZTYwNzU3MDIz._V1_.jpg",
        "width": "267",
        "height": "400",
        "caption": ""
      },
      "category": "actress",
      "characters": [
        "Murph (Older)"
      ]
    },
    {
      "id": "nm0001475",
      "name": "John Lithgow",
      "image": {
        "id": "",
        "url": "https://m.media-amazon.com/images/M/MV5BMTQzMzUyNDkzNF5BMl5BanBnXkFtZTcwNTMwNTU5MQ@@._V1_.jpg",
        "width": "319",
        "height": "400",
        "caption": ""
      },
      "category": "actor",
      "characters": [
        "Donald"
      ]
    },
    {
      "id": "nm3154303",
      "name": "Timothée Chalamet",
      "image": {
        "id": "",
        "url": "https://m.media-amazon.com/images/M/MV5BNThiOTM4NTAtMDczNy00YzlkLWJhNTEtZTZhNDVmYzlkZWI0XkEyXkFqcGdeQXVyMTEyMjM2NDc2._V1_.jpg",
        "width": "3000",
        "height": "2265",
        "caption": ""
      },
      "category": "actor",
      "characters": [
        "Tom (15 Yrs.)"
      ]
    },
    {
      "id": "nm0654648",
      "name": "David Oyelowo",
      "image": {
        "id": "",
        "url": "https://m.media-amazon.com/images/M/MV5BOTMyODkxMzktNWMwMy00MjRlLTlmNjUtM2I0NTczZDU3YjE0XkEyXkFqcGdeQXVyOTkyMDQ2Mw@@._V1_.jpg",
        "width": "788",
        "height": "1024",
        "caption": ""
      },
      "category": "actor",
      "characters": [
        "School Principal"
      ]
    },
    {
      "id": "nm2180792",
      "name": "Collette Wolfe",
      "image": {
        "id": "",
        "url": "https://m.media-amazon.com/images/M/MV5BMjIwMDc5Mzk5MV5BMl5BanBnXkFtZTcwMjMzMTUxMw@@._V1_.jpg",
        "width": "701",
        "height": "1024",
        "caption": ""
      },
      "category": "actress",
      "characters": [
        "Ms. Hanley"
      ]
    },
    {
      "id": "nm0565133",
      "name": "Francis X. McCarthy",
      "image": {
        "id": "",
        "url": "https://m.media-amazon.com/images/M/MV5BMzE5OTg2MzA4Nl5BMl5BanBnXkFtZTcwMTc3NDM1Nw@@._V1_.jpg",
        "width": "741",
        "height": "491",
        "caption": ""
      },
      "category": "actor",
      "characters": [
        "Boots"
      ]
    },
    {
      "id": "nm0410347",
      "name": "Bill Irwin",
      "image": {
        "id": "",
        "url": "https://m.media-amazon.com/images/M/MV5BMTU3NjI4MzM4NV5BMl5BanBnXkFtZTYwNzk4NTc4._V1_.jpg",
        "width": "273",
        "height": "400",
        "caption": ""
      },
      "category": "actor",
      "characters": [
        "TARS"
      ]
    },
    {
      "id": "nm0095960",
      "name": "Andrew Borba",
      "image": {
        "id": "",
        "url": "https://m.media-amazon.com/images/M/MV5BMTEyODY4NjExMjVeQTJeQWpwZ15BbWU3MDQ5MjA3Njk@._V1_.jpg",
        "width": "426",
        "height": "639",
        "caption": ""
      },
      "category": "actor",
      "characters": [
        "Smith"
      ]
    },
    {
      "id": "nm0004747",
      "name": "Wes Bentley",
      "image": {
        "id": "",
        "url": "https://m.media-amazon.com/images/M/MV5BOTgyOTY5OTA5OF5BMl5BanBnXkFtZTcwNzM1MjM1Nw@@._V1_.jpg",
        "width": "1373",
        "height": "2048",
        "caption": ""
      },
      "category": "actor",
      "characters": [
        "Doyle"
      ]
    },
    {
      "id": "nm0001137",
      "name": "William Devane",
      "image": {
        "id": "",
        "url": "https://m.media-amazon.com/images/M/MV5BMTkwOTE2NDIyNV5BMl5BanBnXkFtZTgwOTE1MTQ2NjE@._V1_.jpg",
        "width": "1462",
        "height": "1950",
        "caption": ""
      },
      "category": "actor",
      "characters": [
        "Williams"
      ]
    },
    {
      "id": "nm0000323",
      "name": "Michael Caine",
      "image": {
        "id": "",
        "url": "https://m.media-amazon.com/images/M/MV5BMjAwNzIwNTQ4Ml5BMl5BanBnXkFtZTYwMzE1MTUz._V1_.jpg",
        "width": "288",
        "height": "400",
        "caption": ""
      },
      "category": "actor",
      "characters": [
        "Professor Brand"
      ]
    },
    {
      "id": "nm1408543",
      "name": "David Gyasi",
      "image": {
        "id": "",
        "url": "https://m.media-amazon.com/images/M/MV5BNGI0YjQ5MzktZmI1My00YWU2LThjNmUtNTU3YmMyYTIwNjY4XkEyXkFqcGdeQXVyMjI3NzY0NTA@._V1_.jpg",
        "width": "1064",
        "height": "1600",
        "caption": ""
      },
      "category": "actor",
      "characters": [
        "Romilly"
      ]
    },
    {
      "id": "nm1577637",
      "name": "Josh Stewart",
      "image": {
        "id": "",
        "url": "https://m.media-amazon.com/images/M/MV5BMGU3YmYzMDMtNGVjOC00MjQ0LWFiMjMtMjc3YWZmOGI4NzJjXkEyXkFqcGdeQXVyMzA5MDIzOTE@._V1_.jpg",
        "width": "1052",
        "height": "1404",
        "caption": ""
      },
      "category": "actor",
      "characters": [
        "CASE"
      ]
    },
    {
      "id": "nm0000729",
      "name": "Casey Affleck",
      "image": {
        "id": "",
        "url": "https://m.media-amazon.com/images/M/MV5BZjg5ZWM2ZTYtOGM1Yi00NzY4LThlMDctMWM4NTdlYTRhODRhXkEyXkFqcGdeQXVyMTYzOTczNTY5._V1_.jpg",
        "width": "986",
        "height": "1280",
        "caption": ""
      },
      "category": "actor",
      "characters": [
        "Tom"
      ]
    }
  ],
  "genres": [
    {
      "id": "Adventure",
      "name": "Adventure"
    },
    {
      "id": "Drama",
      "name": "Drama"
    },
    {
      "id": "Sci-Fi",
      "name": "Sci-Fi"
    }
  ],
  "companies": [
    {
      "id": "co0023400",
      "name": "Paramount Pictures"
    },
    {
      "id": "co0002663",
      "name": "Warner Bros."
    },
    {
      "id": "co0159111",
      "name": "Legendary Entertainment"
    }
  ],
  "countries": [
    {
      "id": "US",
      "name": "United States"
    },
    {
      "id": "GB",
      "name": "United Kingdom"
    },
    {
      "id": "CA",
      "name": "Canada"
    }
  ],
  "languages": [
    {
      "id": "en",
      "name": "English"
    }
  ],
  "contentRating": "T",
  "rating": {
    "voteCount": "2007461",
    "difference": "11",
    "currentRank": "96",
    "changeDirection": "UP",
    "aggregateRating": "8,7",
    "voteCountText": ""
  },
  "metascore": "74",
  "videos": [
    {
      "id": "vi1586278169",
      "runtime": {
        "seconds": "148",
        "plainText": "2m 28s"
      },
      "name": "Trailer #4",
      "description": "A group of explorers make use of a newly discovered wormhole to surpass the limitations on human space travel and conquer the vast distances involved in an interstellar voyage.\r\n",
      "thumbnail": {
        "id": "",
        "url": "https://m.media-amazon.com/images/M/MV5BNjM5OTQzMTg4N15BMl5BanBnXkFtZTgwOTgyMjM0NTE@._V1_.jpg",
        "width": "640",
        "height": "478",
        "caption": ""
      },
      "type": "Trailer",
      "playbackURLs": [
        {
          "quality": "480p",
          "language": "en-US",
          "mimeType": "",
          "url": "https://imdb-video.media-imdb.com/vi1586278169/1434659607842-pgv4ql-1616202363366.mp4?Expires=1699313076&Signature=evrcA81M-MU625YsS6izE5UyXdb1azPzH6t14fN0ucpwKTpSsL1pM6Rmecw6SPX2iZi3NtI-McyMc~LGpTTlGWgrEicqU7lTmWTkE~HLzDvtlTOGauYoMMLq5peE8KvyKk~f14sHFVOOAC2FiH7rfGbapmbgeWNiOwTkVLbwtbJMOLp3mjeuZgHe2bGbodMYXDdYqM9Zc-vQylAS5P34yasbPU3M4W24YtpMYHvN0C07M9Hvr~sQfXg247kM8Sl~NgJQ0etoHwQ7xd~yZEYtfB9W1VVomo9CCy2xU64SLlkIAOX4yRt7~LVPRJsI4XDIdzfKv2FJz78n8JT1MKOLVA__&Key-Pair-Id=APKAIFLZBVQZ24NQH3KA"
        },
        {
          "quality": "SD",
          "language": "en-US",
          "mimeType": "",
          "url": "https://imdb-video.media-imdb.com/vi1586278169/1434659454657-dx9ykf-1616202363366.mp4?Expires=1699313076&Signature=QGzPdgSzNpAmQflkGyeLClKI482eEBjzU4VdeJ0BIU9Ugy6LzEysN-WLglYfGB9Sm2h-ctntX7hOXw5hnlo-PqKR33gUpaHt9KBCaUW-w3ir5SUlttJhdkvz6H~nLvCW9Ro7nv7Wvjzi3c7WDs5cGxE3wRhcx0PDChbMv2EPVBNcddhPPEeC-OcpM~dSZwJHyEvEC8tA22~McxQxfRXLv2HtPb1CPE2SWNnWf8jI6dfrsqclkIWrseSB9pOTL5ZCR2yPYPhwMqEah~fMviAa1hcUvQB~PqZOcNHO9n-Fa5-oK2P8VLhzb3n2KCK03TMurW2KUbks6Kae62xuKm~bAg__&Key-Pair-Id=APKAIFLZBVQZ24NQH3KA"
        }
      ]
    },
    {
      "id": "vi2284039193",
      "runtime": {
        "seconds": "154",
        "plainText": "2m 34s"
      },
      "name": "Trailer #3",
      "description": "A group of explorers make use of a newly discovered wormhole to surpass the limitations on human space travel and conquer the vast distances involved in an interstellar voyage.\r\n",
      "thumbnail": {
        "id": "",
        "url": "https://m.media-amazon.com/images/M/MV5BODcyMjM0OTUxNF5BMl5BanBnXkFtZTgwMjM2OTMyMzE@._V1_.jpg",
        "width": "1280",
        "height": "720",
        "caption": ""
      },
      "type": "Trailer",
      "playbackURLs": [
        {
          "quality": "480p",
          "language": "en-US",
          "mimeType": "",
          "url": "https://imdb-video.media-imdb.com/vi2284039193/1434659607842-pgv4ql-1616198542203.mp4?Expires=1699313076&Signature=tCeAZRW1UsmDEvAtrW0Q2TbsymrOIZpTW3ji1Fxf0uWLNigGePlMm~gziYOBIbPSm-pj0w8l4M7yfXFMCxHvI5fVPY7C~2xjV5HCQLslUF0Pro41s0QZrL1TmnHyQ3MiOVpI2CTFCP-dABtesFz6A0ikXdVhWswYDgH55-Fq8XZir7RhiFuiBA3-CBJuNjgL8Ptc6rVdMq5DN9kFnFNrNTdaC0kdd6UNX7tA5~qe3fwARv19OlmsFM9VfmzL0djAlVMwlzjqkbC9TEkzGHjfXCCgS-iEk06nPZpK3hVdGltsNATc5VTssniYbzzNK86MO45Ng52CskPD~1~QcbSY8A__&Key-Pair-Id=APKAIFLZBVQZ24NQH3KA"
        },
        {
          "quality": "SD",
          "language": "en-US",
          "mimeType": "",
          "url": "https://imdb-video.media-imdb.com/vi2284039193/1434659454657-dx9ykf-1616198542203.mp4?Expires=1699313076&Signature=sPcR7wwuG9MtTIRBNUsGKJLUQBgGbjsSyU1~c2GF0O7BOHGayQQFIsli6C~3k7pWsyKs7oH1K0GL0sbo9eCrFo-5SmCBcXx04OkCaVhHKJR~1iBICZpC6L9r6uhr2TSWFZky-P6KnrQ~ICHe7ScU-5cn746Z-4ALkBL67ri7aDbCyEEhkqIVUXAXsWsRVWlc7eONEkC7grfq0qhLNSh8w1wKc-8RBvJmd1vaicc1powZge0voQZLc2KPZJyJbpnZT0B7A7hZwQJUbhkf8T4A9bJOrmGN5-C8b0BK14W8EFO9yoKEs2vVvuKLuKGUhbONY2dTBG52q8L2cagHYHPv0g__&Key-Pair-Id=APKAIFLZBVQZ24NQH3KA"
        }
      ]
    }
  ],
  "photos": [
    {
      "id": "rm2841711616",
      "url": "https://m.media-amazon.com/images/M/MV5BMTc0NDQ4MjkyOF5BMl5BanBnXkFtZTgwNDE2NzUzOTE@._V1_.jpg",
      "width": "1024",
      "height": "768",
      "caption": "Bill Irwin in Interstellar (2014)"
    },
    {
      "id": "rm1728576256",
      "url": "https://m.media-amazon.com/images/M/MV5BMjA3NTEwOTMxMV5BMl5BanBnXkFtZTgwMjMyODgxMzE@._V1_.jpg",
      "width": "2048",
      "height": "1365",
      "caption": "Matthew McConaughey in Interstellar (2014)"
    },
    {
      "id": "rm1711799040",
      "url": "https://m.media-amazon.com/images/M/MV5BMTQ0MjQ3NjE1MF5BMl5BanBnXkFtZTgwMzMyODgxMzE@._V1_.jpg",
      "width": "2048",
      "height": "1365",
      "caption": "Matthew McConaughey in Interstellar (2014)"
    },
    {
      "id": "rm419953408",
      "url": "https://m.media-amazon.com/images/M/MV5BMTg4MTY3MDUyNl5BMl5BanBnXkFtZTgwMDMyODgxMzE@._V1_.jpg",
      "width": "2048",
      "height": "1365",
      "caption": "Jessica Chastain in Interstellar (2014)"
    },
    {
      "id": "rm403176192",
      "url": "https://m.media-amazon.com/images/M/MV5BMzQ5ODE2MzEwM15BMl5BanBnXkFtZTgwMTMyODgxMzE@._V1_.jpg",
      "width": "2048",
      "height": "1365",
      "caption": "Matthew McConaughey, Michael Caine, and Mackenzie Foy in Interstellar (2014)"
    },
    {
      "id": "rm453507840",
      "url": "https://m.media-amazon.com/images/M/MV5BMzk3MzIzNzM5NV5BMl5BanBnXkFtZTgwNzIyODgxMzE@._V1_.jpg",
      "width": "2048",
      "height": "1365",
      "caption": "David Gyasi in Interstellar (2014)"
    },
    {
      "id": "rm436730624",
      "url": "https://m.media-amazon.com/images/M/MV5BMjI2OTg1NjUxM15BMl5BanBnXkFtZTgwOTIyODgxMzE@._V1_.jpg",
      "width": "2048",
      "height": "1365",
      "caption": "Matthew McConaughey and David Gyasi in Interstellar (2014)"
    },
    {
      "id": "rm470285056",
      "url": "https://m.media-amazon.com/images/M/MV5BMTAyOTI5MTg5MDFeQTJeQWpwZ15BbWU4MDYyMjg4MTMx._V1_.jpg",
      "width": "2048",
      "height": "1365",
      "caption": "Matthew McConaughey and Anne Hathaway in Interstellar (2014)"
    },
    {
      "id": "rm503839488",
      "url": "https://m.media-amazon.com/images/M/MV5BMTg4Njk4MzY0Nl5BMl5BanBnXkFtZTgwMzIyODgxMzE@._V1_.jpg",
      "width": "2048",
      "height": "1365",
      "caption": "Jessica Chastain in Interstellar (2014)"
    },
    {
      "id": "rm487062272",
      "url": "https://m.media-amazon.com/images/M/MV5BOTc0NDkxNTkwMF5BMl5BanBnXkFtZTgwNDIyODgxMzE@._V1_.jpg",
      "width": "2048",
      "height": "1365",
      "caption": "Wes Bentley in Interstellar (2014)"
    },
    {
      "id": "rm268958464",
      "url": "https://m.media-amazon.com/images/M/MV5BNjQ2NTk3NTQ5OF5BMl5BanBnXkFtZTgwMTIyODgxMzE@._V1_.jpg",
      "width": "2048",
      "height": "1365",
      "caption": "Matthew McConaughey, Timothée Chalamet, and Mackenzie Foy in Interstellar (2014)"
    },
    {
      "id": "rm520616704",
      "url": "https://m.media-amazon.com/images/M/MV5BMTc0MjI0NzI0MV5BMl5BanBnXkFtZTgwMjIyODgxMzE@._V1_.jpg",
      "width": "2048",
      "height": "1365",
      "caption": "Anne Hathaway and Wes Bentley in Interstellar (2014)"
    },
    {
      "id": "rm302512896",
      "url": "https://m.media-amazon.com/images/M/MV5BODg1Njg1ODQ2Ml5BMl5BanBnXkFtZTgwODEyODgxMzE@._V1_.jpg",
      "width": "2048",
      "height": "1365",
      "caption": "Topher Grace and Jessica Chastain in Interstellar (2014)"
    }
  ],
  "boxOffice": {
    "lifetimeGross": {
      "amount": "188020017",
      "currency": "USD",
      "amountText": ""
    },
    "worldwideGross": {
      "amount": "703170837",
      "currency": "USD",
      "amountText": ""
    },
    "productionBudget": {
      "amount": "165000000",
      "currency": "USD",
      "amountText": ""
    },
    "openingWeekendGross": {
      "amount": "47510360",
      "currency": "USD",
      "amountText": ""
    }
  },
  "keywords": [
    "saving the world",
    "astronaut",
    "space travel",
    "father daughter relationship",
    "wormhole"
  ],
  "similars": [
    {
      "id": "tt1375666",
      "title": "Inception",
      "originalTitle": "Inception",
      "type": "Movie",
      "year": "2010",
      "image": {
        "id": "rm1102869504",
        "url": "https://m.media-amazon.com/images/M/MV5BMWVjOTM1MzAtOTk3ZS00YmUyLThkNDgtNDhhNjU4YTI2MWMxXkEyXkFqcGdeQXVyMTYzMDM0NTU@._V1_.jpg",
        "width": "1261",
        "height": "1802",
        "caption": "Leonardo DiCaprio, Joseph Gordon-Levitt, Tom Hardy, Elliot Page, Ken Watanabe, and Dileep Rao in Inception (2010)"
      },
      "releaseDate": "",
      "runtime": {
        "seconds": "8880",
        "plainText": "2h 28m"
      },
      "plot": "",
      "awards": {
        "rank": "",
        "wins": "",
        "eventId": "",
        "awardId": "",
        "awardText": "",
        "awardWins": "",
        "awardNominations": "",
        "nomination": ""
      },
      "directors": [],
      "writers": [],
      "actors": [],
      "genres": [
        {
          "id": "Action",
          "name": "Action"
        },
        {
          "id": "Adventure",
          "name": "Adventure"
        },
        {
          "id": "Sci-Fi",
          "name": "Sci-Fi"
        }
      ],
      "companies": [],
      "countries": [],
      "languages": [],
      "contentRating": "T",
      "rating": {
        "voteCount": "2482733",
        "difference": "",
        "currentRank": "",
        "changeDirection": "",
        "aggregateRating": "8,8",
        "voteCountText": ""
      },
      "metascore": "",
      "videos": [],
      "photos": [],
      "boxOffice": {
        "lifetimeGross": {
          "amount": "",
          "currency": "",
          "amountText": ""
        },
        "worldwideGross": {
          "amount": "",
          "currency": "",
          "amountText": ""
        },
        "productionBudget": {
          "amount": "",
          "currency": "",
          "amountText": ""
        },
        "openingWeekendGross": {
          "amount": "",
          "currency": "",
          "amountText": ""
        }
      },
      "keywords": [],
      "similars": [],
      "episodes": {
        "total": "",
        "seasons": ""
      },
      "errorMessage": ""
    },
    {
      "id": "tt0468569",
      "title": "Il cavaliere oscuro",
      "originalTitle": "The Dark Knight",
      "type": "Movie",
      "year": "2008",
      "image": {
        "id": "rm4023877632",
        "url": "https://m.media-amazon.com/images/M/MV5BMTMxNTMwODM0NF5BMl5BanBnXkFtZTcwODAyMTk2Mw@@._V1_.jpg",
        "width": "1383",
        "height": "2048",
        "caption": "Morgan Freeman, Gary Oldman, Christian Bale, Michael Caine, Aaron Eckhart, Heath Ledger, Maggie Gyllenhaal, nm0614165, and Chin Han in Il cavaliere oscuro (2008)"
      },
      "releaseDate": "",
      "runtime": {
        "seconds": "9120",
        "plainText": "2h 32m"
      },
      "plot": "",
      "awards": {
        "rank": "",
        "wins": "",
        "eventId": "",
        "awardId": "",
        "awardText": "",
        "awardWins": "",
        "awardNominations": "",
        "nomination": ""
      },
      "directors": [],
      "writers": [],
      "actors": [],
      "genres": [
        {
          "id": "Action",
          "name": "Action"
        },
        {
          "id": "Crime",
          "name": "Crime"
        },
        {
          "id": "Drama",
          "name": "Drama"
        }
      ],
      "companies": [],
      "countries": [],
      "languages": [],
      "contentRating": "T",
      "rating": {
        "voteCount": "2797185",
        "difference": "",
        "currentRank": "",
        "changeDirection": "",
        "aggregateRating": "9",
        "voteCountText": ""
      },
      "metascore": "",
      "videos": [],
      "photos": [],
      "boxOffice": {
        "lifetimeGross": {
          "amount": "",
          "currency": "",
          "amountText": ""
        },
        "worldwideGross": {
          "amount": "",
          "currency": "",
          "amountText": ""
        },
        "productionBudget": {
          "amount": "",
          "currency": "",
          "amountText": ""
        },
        "openingWeekendGross": {
          "amount": "",
          "currency": "",
          "amountText": ""
        }
      },
      "keywords": [],
      "similars": [],
      "episodes": {
        "total": "",
        "seasons": ""
      },
      "errorMessage": ""
    },
    {
      "id": "tt0137523",
      "title": "Fight Club",
      "originalTitle": "Fight Club",
      "type": "Movie",
      "year": "1999",
      "image": {
        "id": "rm713059072",
        "url": "https://m.media-amazon.com/images/M/MV5BNTExODU3YzYtNzlkMC00NzZlLWFkODYtZmFmMTc2MjZhOWRmXkEyXkFqcGdeQXVyODIyOTEyMzY@._V1_.jpg",
        "width": "420",
        "height": "594",
        "caption": "Brad Pitt and Edward Norton in Fight Club (1999)"
      },
      "releaseDate": "",
      "runtime": {
        "seconds": "8340",
        "plainText": "2h 19m"
      },
      "plot": "",
      "awards": {
        "rank": "",
        "wins": "",
        "eventId": "",
        "awardId": "",
        "awardText": "",
        "awardWins": "",
        "awardNominations": "",
        "nomination": ""
      },
      "directors": [],
      "writers": [],
      "actors": [],
      "genres": [
        {
          "id": "Drama",
          "name": "Drama"
        }
      ],
      "companies": [],
      "countries": [],
      "languages": [],
      "contentRating": "VM14",
      "rating": {
        "voteCount": "2250518",
        "difference": "",
        "currentRank": "",
        "changeDirection": "",
        "aggregateRating": "8,8",
        "voteCountText": ""
      },
      "metascore": "",
      "videos": [],
      "photos": [],
      "boxOffice": {
        "lifetimeGross": {
          "amount": "",
          "currency": "",
          "amountText": ""
        },
        "worldwideGross": {
          "amount": "",
          "currency": "",
          "amountText": ""
        },
        "productionBudget": {
          "amount": "",
          "currency": "",
          "amountText": ""
        },
        "openingWeekendGross": {
          "amount": "",
          "currency": "",
          "amountText": ""
        }
      },
      "keywords": [],
      "similars": [],
      "episodes": {
        "total": "",
        "seasons": ""
      },
      "errorMessage": ""
    },
    {
      "id": "tt0109830",
      "title": "Forrest Gump",
      "originalTitle": "Forrest Gump",
      "type": "Movie",
      "year": "1994",
      "image": {
        "id": "rm3744625664",
        "url": "https://m.media-amazon.com/images/M/MV5BY2I0NTI3MGEtYWJiMy00YmM3LWI2ZWYtY2YwYjRkMmUwNzliXkEyXkFqcGdeQXVyMTYzMDM0NTU@._V1_.jpg",
        "width": "220",
        "height": "330",
        "caption": "Tom Hanks and Mykelti Williamson in Forrest Gump (1994)"
      },
      "releaseDate": "",
      "runtime": {
        "seconds": "8520",
        "plainText": "2h 22m"
      },
      "plot": "",
      "awards": {
        "rank": "",
        "wins": "",
        "eventId": "",
        "awardId": "",
        "awardText": "",
        "awardWins": "",
        "awardNominations": "",
        "nomination": ""
      },
      "directors": [],
      "writers": [],
      "actors": [],
      "genres": [
        {
          "id": "Drama",
          "name": "Drama"
        },
        {
          "id": "Romance",
          "name": "Romance"
        }
      ],
      "companies": [],
      "countries": [],
      "languages": [],
      "contentRating": "T",
      "rating": {
        "voteCount": "2192643",
        "difference": "",
        "currentRank": "",
        "changeDirection": "",
        "aggregateRating": "8,8",
        "voteCountText": ""
      },
      "metascore": "",
      "videos": [],
      "photos": [],
      "boxOffice": {
        "lifetimeGross": {
          "amount": "",
          "currency": "",
          "amountText": ""
        },
        "worldwideGross": {
          "amount": "",
          "currency": "",
          "amountText": ""
        },
        "productionBudget": {
          "amount": "",
          "currency": "",
          "amountText": ""
        },
        "openingWeekendGross": {
          "amount": "",
          "currency": "",
          "amountText": ""
        }
      },
      "keywords": [],
      "similars": [],
      "episodes": {
        "total": "",
        "seasons": ""
      },
      "errorMessage": ""
    },
    {
      "id": "tt0111161",
      "title": "Le ali della libertà",
      "originalTitle": "The Shawshank Redemption",
      "type": "Movie",
      "year": "1994",
      "image": {
        "id": "rm1690056449",
        "url": "https://m.media-amazon.com/images/M/MV5BNDE3ODcxYzMtY2YzZC00NmNlLWJiNDMtZDViZWM2MzIxZDYwXkEyXkFqcGdeQXVyNjAwNDUxODI@._V1_.jpg",
        "width": "1200",
        "height": "1800",
        "caption": "Tim Robbins in Le ali della libertà (1994)"
      },
      "releaseDate": "",
      "runtime": {
        "seconds": "8520",
        "plainText": "2h 22m"
      },
      "plot": "",
      "awards": {
        "rank": "",
        "wins": "",
        "eventId": "",
        "awardId": "",
        "awardText": "",
        "awardWins": "",
        "awardNominations": "",
        "nomination": ""
      },
      "directors": [],
      "writers": [],
      "actors": [],
      "genres": [
        {
          "id": "Drama",
          "name": "Drama"
        }
      ],
      "companies": [],
      "countries": [],
      "languages": [],
      "contentRating": "T",
      "rating": {
        "voteCount": "2815662",
        "difference": "",
        "currentRank": "",
        "changeDirection": "",
        "aggregateRating": "9,3",
        "voteCountText": ""
      },
      "metascore": "",
      "videos": [],
      "photos": [],
      "boxOffice": {
        "lifetimeGross": {
          "amount": "",
          "currency": "",
          "amountText": ""
        },
        "worldwideGross": {
          "amount": "",
          "currency": "",
          "amountText": ""
        },
        "productionBudget": {
          "amount": "",
          "currency": "",
          "amountText": ""
        },
        "openingWeekendGross": {
          "amount": "",
          "currency": "",
          "amountText": ""
        }
      },
      "keywords": [],
      "similars": [],
      "episodes": {
        "total": "",
        "seasons": ""
      },
      "errorMessage": ""
    },
    {
      "id": "tt7286456",
      "title": "Joker",
      "originalTitle": "Joker",
      "type": "Movie",
      "year": "2019",
      "image": {
        "id": "rm3922107649",
        "url": "https://m.media-amazon.com/images/M/MV5BZjRjYzhkMjAtMmZmOS00MGM5LTliNzMtNThjYWJhMzM3NmI2XkEyXkFqcGdeQXVyODc0OTEyNDU@._V1_.jpg",
        "width": "420",
        "height": "622",
        "caption": "Joaquin Phoenix in Joker (2019)"
      },
      "releaseDate": "",
      "runtime": {
        "seconds": "7320",
        "plainText": "2h 2m"
      },
      "plot": "",
      "awards": {
        "rank": "",
        "wins": "",
        "eventId": "",
        "awardId": "",
        "awardText": "",
        "awardWins": "",
        "awardNominations": "",
        "nomination": ""
      },
      "directors": [],
      "writers": [],
      "actors": [],
      "genres": [
        {
          "id": "Crime",
          "name": "Crime"
        },
        {
          "id": "Drama",
          "name": "Drama"
        },
        {
          "id": "Thriller",
          "name": "Thriller"
        }
      ],
      "companies": [],
      "countries": [],
      "languages": [],
      "contentRating": "VM14",
      "rating": {
        "voteCount": "1416572",
        "difference": "",
        "currentRank": "",
        "changeDirection": "",
        "aggregateRating": "8,4",
        "voteCountText": ""
      },
      "metascore": "",
      "videos": [],
      "photos": [],
      "boxOffice": {
        "lifetimeGross": {
          "amount": "",
          "currency": "",
          "amountText": ""
        },
        "worldwideGross": {
          "amount": "",
          "currency": "",
          "amountText": ""
        },
        "productionBudget": {
          "amount": "",
          "currency": "",
          "amountText": ""
        },
        "openingWeekendGross": {
          "amount": "",
          "currency": "",
          "amountText": ""
        }
      },
      "keywords": [],
      "similars": [],
      "episodes": {
        "total": "",
        "seasons": ""
      },
      "errorMessage": ""
    },
    {
      "id": "tt0993846",
      "title": "The Wolf of Wall Street",
      "originalTitle": "The Wolf of Wall Street",
      "type": "Movie",
      "year": "2013",
      "image": {
        "id": "rm2842940160",
        "url": "https://m.media-amazon.com/images/M/MV5BMjIxMjgxNTk0MF5BMl5BanBnXkFtZTgwNjIyOTg2MDE@._V1_.jpg",
        "width": "1382",
        "height": "2048",
        "caption": "Leonardo DiCaprio and Jonah Hill in The Wolf of Wall Street (2013)"
      },
      "releaseDate": "",
      "runtime": {
        "seconds": "10800",
        "plainText": "3h 0m"
      },
      "plot": "",
      "awards": {
        "rank": "",
        "wins": "",
        "eventId": "",
        "awardId": "",
        "awardText": "",
        "awardWins": "",
        "awardNominations": "",
        "nomination": ""
      },
      "directors": [],
      "writers": [],
      "actors": [],
      "genres": [
        {
          "id": "Biography",
          "name": "Biography"
        },
        {
          "id": "Comedy",
          "name": "Comedy"
        },
        {
          "id": "Crime",
          "name": "Crime"
        }
      ],
      "companies": [],
      "countries": [],
      "languages": [],
      "contentRating": "VM14",
      "rating": {
        "voteCount": "1523249",
        "difference": "",
        "currentRank": "",
        "changeDirection": "",
        "aggregateRating": "8,2",
        "voteCountText": ""
      },
      "metascore": "",
      "videos": [],
      "photos": [],
      "boxOffice": {
        "lifetimeGross": {
          "amount": "",
          "currency": "",
          "amountText": ""
        },
        "worldwideGross": {
          "amount": "",
          "currency": "",
          "amountText": ""
        },
        "productionBudget": {
          "amount": "",
          "currency": "",
          "amountText": ""
        },
        "openingWeekendGross": {
          "amount": "",
          "currency": "",
          "amountText": ""
        }
      },
      "keywords": [],
      "similars": [],
      "episodes": {
        "total": "",
        "seasons": ""
      },
      "errorMessage": ""
    },
    {
      "id": "tt0114369",
      "title": "Seven",
      "originalTitle": "Se7en",
      "type": "Movie",
      "year": "1995",
      "image": {
        "id": "rm3116368640",
        "url": "https://m.media-amazon.com/images/M/MV5BOTUwODM5MTctZjczMi00OTk4LTg3NWUtNmVhMTAzNTNjYjcyXkEyXkFqcGdeQXVyNjU0OTQ0OTY@._V1_.jpg",
        "width": "1801",
        "height": "2815",
        "caption": "Brad Pitt and Morgan Freeman in Seven (1995)"
      },
      "releaseDate": "",
      "runtime": {
        "seconds": "7620",
        "plainText": "2h 7m"
      },
      "plot": "",
      "awards": {
        "rank": "",
        "wins": "",
        "eventId": "",
        "awardId": "",
        "awardText": "",
        "awardWins": "",
        "awardNominations": "",
        "nomination": ""
      },
      "directors": [],
      "writers": [],
      "actors": [],
      "genres": [
        {
          "id": "Crime",
          "name": "Crime"
        },
        {
          "id": "Drama",
          "name": "Drama"
        },
        {
          "id": "Mystery",
          "name": "Mystery"
        }
      ],
      "companies": [],
      "countries": [],
      "languages": [],
      "contentRating": "T",
      "rating": {
        "voteCount": "1745809",
        "difference": "",
        "currentRank": "",
        "changeDirection": "",
        "aggregateRating": "8,6",
        "voteCountText": ""
      },
      "metascore": "",
      "videos": [],
      "photos": [],
      "boxOffice": {
        "lifetimeGross": {
          "amount": "",
          "currency": "",
          "amountText": ""
        },
        "worldwideGross": {
          "amount": "",
          "currency": "",
          "amountText": ""
        },
        "productionBudget": {
          "amount": "",
          "currency": "",
          "amountText": ""
        },
        "openingWeekendGross": {
          "amount": "",
          "currency": "",
          "amountText": ""
        }
      },
      "keywords": [],
      "similars": [],
      "episodes": {
        "total": "",
        "seasons": ""
      },
      "errorMessage": ""
    },
    {
      "id": "tt0133093",
      "title": "Matrix",
      "originalTitle": "The Matrix",
      "type": "Movie",
      "year": "1999",
      "image": {
        "id": "rm2100392960",
        "url": "https://m.media-amazon.com/images/M/MV5BNTRhZGNiZTUtZTUxMi00NWQ2LWI1NmUtZDRjMzAxNjUzZTQ5XkEyXkFqcGdeQXVyMTYzMDM0NTU@._V1_.jpg",
        "width": "220",
        "height": "330",
        "caption": "Keanu Reeves, Laurence Fishburne, and Carrie-Anne Moss in Matrix (1999)"
      },
      "releaseDate": "",
      "runtime": {
        "seconds": "8160",
        "plainText": "2h 16m"
      },
      "plot": "",
      "awards": {
        "rank": "",
        "wins": "",
        "eventId": "",
        "awardId": "",
        "awardText": "",
        "awardWins": "",
        "awardNominations": "",
        "nomination": ""
      },
      "directors": [],
      "writers": [],
      "actors": [],
      "genres": [
        {
          "id": "Action",
          "name": "Action"
        },
        {
          "id": "Sci-Fi",
          "name": "Sci-Fi"
        }
      ],
      "companies": [],
      "countries": [],
      "languages": [],
      "contentRating": "T",
      "rating": {
        "voteCount": "2001801",
        "difference": "",
        "currentRank": "",
        "changeDirection": "",
        "aggregateRating": "8,7",
        "voteCountText": ""
      },
      "metascore": "",
      "videos": [],
      "photos": [],
      "boxOffice": {
        "lifetimeGross": {
          "amount": "",
          "currency": "",
          "amountText": ""
        },
        "worldwideGross": {
          "amount": "",
          "currency": "",
          "amountText": ""
        },
        "productionBudget": {
          "amount": "",
          "currency": "",
          "amountText": ""
        },
        "openingWeekendGross": {
          "amount": "",
          "currency": "",
          "amountText": ""
        }
      },
      "keywords": [],
      "similars": [],
      "episodes": {
        "total": "",
        "seasons": ""
      },
      "errorMessage": ""
    },
    {
      "id": "tt0110912",
      "title": "Pulp Fiction",
      "originalTitle": "Pulp Fiction",
      "type": "Movie",
      "year": "1994",
      "image": {
        "id": "rm4048647168",
        "url": "https://m.media-amazon.com/images/M/MV5BMDAyOWI5ZGItMDM2MS00M2ZiLWE1OWItYzI1ZGFhYTVlMzhjXkEyXkFqcGdeQXVyMTYzMDM0NTU@._V1_.jpg",
        "width": "722",
        "height": "1000",
        "caption": "Uma Thurman in Pulp Fiction (1994)"
      },
      "releaseDate": "",
      "runtime": {
        "seconds": "9240",
        "plainText": "2h 34m"
      },
      "plot": "",
      "awards": {
        "rank": "",
        "wins": "",
        "eventId": "",
        "awardId": "",
        "awardText": "",
        "awardWins": "",
        "awardNominations": "",
        "nomination": ""
      },
      "directors": [],
      "writers": [],
      "actors": [],
      "genres": [
        {
          "id": "Crime",
          "name": "Crime"
        },
        {
          "id": "Drama",
          "name": "Drama"
        }
      ],
      "companies": [],
      "countries": [],
      "languages": [],
      "contentRating": "VM18",
      "rating": {
        "voteCount": "2159313",
        "difference": "",
        "currentRank": "",
        "changeDirection": "",
        "aggregateRating": "8,9",
        "voteCountText": ""
      },
      "metascore": "",
      "videos": [],
      "photos": [],
      "boxOffice": {
        "lifetimeGross": {
          "amount": "",
          "currency": "",
          "amountText": ""
        },
        "worldwideGross": {
          "amount": "",
          "currency": "",
          "amountText": ""
        },
        "productionBudget": {
          "amount": "",
          "currency": "",
          "amountText": ""
        },
        "openingWeekendGross": {
          "amount": "",
          "currency": "",
          "amountText": ""
        }
      },
      "keywords": [],
      "similars": [],
      "episodes": {
        "total": "",
        "seasons": ""
      },
      "errorMessage": ""
    },
    {
      "id": "tt0482571",
      "title": "The Prestige",
      "originalTitle": "The Prestige",
      "type": "Movie",
      "year": "2006",
      "image": {
        "id": "rm3616020480",
        "url": "https://m.media-amazon.com/images/M/MV5BMGQzMGNiOTEtYjgwZS00ZGNkLWE2NWYtZGY2YzM0Nzg4NDkyXkEyXkFqcGdeQXVyNTIzOTk5ODM@._V1_.jpg",
        "width": "872",
        "height": "1394",
        "caption": "Christian Bale in The Prestige (2006)"
      },
      "releaseDate": "",
      "runtime": {
        "seconds": "7800",
        "plainText": "2h 10m"
      },
      "plot": "",
      "awards": {
        "rank": "",
        "wins": "",
        "eventId": "",
        "awardId": "",
        "awardText": "",
        "awardWins": "",
        "awardNominations": "",
        "nomination": ""
      },
      "directors": [],
      "writers": [],
      "actors": [],
      "genres": [
        {
          "id": "Drama",
          "name": "Drama"
        },
        {
          "id": "Mystery",
          "name": "Mystery"
        },
        {
          "id": "Sci-Fi",
          "name": "Sci-Fi"
        }
      ],
      "companies": [],
      "countries": [],
      "languages": [],
      "contentRating": "T",
      "rating": {
        "voteCount": "1404825",
        "difference": "",
        "currentRank": "",
        "changeDirection": "",
        "aggregateRating": "8,5",
        "voteCountText": ""
      },
      "metascore": "",
      "videos": [],
      "photos": [],
      "boxOffice": {
        "lifetimeGross": {
          "amount": "",
          "currency": "",
          "amountText": ""
        },
        "worldwideGross": {
          "amount": "",
          "currency": "",
          "amountText": ""
        },
        "productionBudget": {
          "amount": "",
          "currency": "",
          "amountText": ""
        },
        "openingWeekendGross": {
          "amount": "",
          "currency": "",
          "amountText": ""
        }
      },
      "keywords": [],
      "similars": [],
      "episodes": {
        "total": "",
        "seasons": ""
      },
      "errorMessage": ""
    },
    {
      "id": "tt1853728",
      "title": "Django Unchained",
      "originalTitle": "Django Unchained",
      "type": "Movie",
      "year": "2012",
      "image": {
        "id": "rm958180352",
        "url": "https://m.media-amazon.com/images/M/MV5BMjIyNTQ5NjQ1OV5BMl5BanBnXkFtZTcwODg1MDU4OA@@._V1_.jpg",
        "width": "1382",
        "height": "2048",
        "caption": "Leonardo DiCaprio, Jamie Foxx, and Christoph Waltz in Django Unchained (2012)"
      },
      "releaseDate": "",
      "runtime": {
        "seconds": "9900",
        "plainText": "2h 45m"
      },
      "plot": "",
      "awards": {
        "rank": "",
        "wins": "",
        "eventId": "",
        "awardId": "",
        "awardText": "",
        "awardWins": "",
        "awardNominations": "",
        "nomination": ""
      },
      "directors": [],
      "writers": [],
      "actors": [],
      "genres": [
        {
          "id": "Drama",
          "name": "Drama"
        },
        {
          "id": "Western",
          "name": "Western"
        }
      ],
      "companies": [],
      "countries": [],
      "languages": [],
      "contentRating": "T",
      "rating": {
        "voteCount": "1645950",
        "difference": "",
        "currentRank": "",
        "changeDirection": "",
        "aggregateRating": "8,5",
        "voteCountText": ""
      },
      "metascore": "",
      "videos": [],
      "photos": [],
      "boxOffice": {
        "lifetimeGross": {
          "amount": "",
          "currency": "",
          "amountText": ""
        },
        "worldwideGross": {
          "amount": "",
          "currency": "",
          "amountText": ""
        },
        "productionBudget": {
          "amount": "",
          "currency": "",
          "amountText": ""
        },
        "openingWeekendGross": {
          "amount": "",
          "currency": "",
          "amountText": ""
        }
      },
      "keywords": [],
      "similars": [],
      "episodes": {
        "total": "",
        "seasons": ""
      },
      "errorMessage": ""
    }
  ],
  "episodes": {
    "total": "",
    "seasons": "0"
  },
  "errorMessage": ""
}
```
