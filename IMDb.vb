Imports System.Net.Http
Imports HtmlAgilityPack
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq

Public Class Environment

    Public Shared Retry As Integer = 0
    Public Shared ReadOnly Property Url As String = "https://www.imdb.com"
    Public Shared ReadOnly Property UserAgent As String = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1; SV1; .NET CLR 1.1.4322; .NET CLR 2.0.50727)"
    Public Shared Function search(qExpression As String, Optional qMode As eSearch = eSearch.All) As Results

        search = New Results

        Dim nUrl As String = Nothing
        Dim nHtml As String = Nothing
        Dim nJson As String = Nothing
        Dim nResults As Results = Nothing

        Try

            Select Case qMode
                Case eSearch.All
                    nUrl = Environment.Url & "/find/?q=" & qExpression & "&ref_=nv_sr_sm"
                Case eSearch.Titles
                    nUrl = Environment.Url & "/find/?s=tt&q=" & qExpression & "&ref_=nv_sr_sm"
                Case eSearch.Episodes
                    nUrl = Environment.Url & "/find/?s=ep&q=" & qExpression & "&ref_=nv_sr_sm"
                Case eSearch.Celebs
                    nUrl = Environment.Url & "/find/?s=nm&q=" & qExpression & "&ref_=nv_sr_sm"
                Case eSearch.Companies
                    nUrl = Environment.Url & "/find/?s=co&q=" & qExpression & "&ref_=nv_sr_sm"
                Case eSearch.Keywords
                    nUrl = Environment.Url & "/find/?s=kw&q=" & qExpression & "&ref_=nv_sr_sm"
                Case Else
                    nUrl = ""
            End Select

            If Not nUrl = "" Then

                nHtml = Http.Html(nUrl)
                nJson = Http.getJson(nHtml)

                nResults = New Results
                If nResults.parseJson(nJson) Then search = nResults

            End If

            Retry = 0

        Catch nException As Exception

            Select Case nException.HResult
                Case -2146233079
                    Retry += 1
                    If Retry < 3 Then search = search(qExpression, qMode)
            End Select

            nException = Nothing

        Finally

            nUrl = Nothing
            nHtml = Nothing
            nJson = Nothing
            nResults = Nothing

            qExpression = Nothing

        End Try

    End Function

    Public Shared Function title(qId As String) As Title

        title = New Title

        Dim nUrl As String = Nothing
        Dim nHtml As String = Nothing
        Dim nJson As String = Nothing
        Dim nMode As String = Nothing
        Dim nTitle As Title = Nothing
        Dim nCode As Integer = Nothing

        Try

            nMode = ""
            If qId.Length > 2 Then
                If Integer.TryParse(qId.Substring(2), nCode) Then
                    nMode = qId.Substring(0, 2)
                End If
            End If

            nUrl = ""
            Select Case nMode
                Case "tt"
                    nUrl = Environment.Url & "/title/" & qId & "/"
                Case "nm"
                    title.errorMessage = "For this identifier, no results could be found. Please try searching for this reference using the 'name' method."
                Case Else
                    title.errorMessage = "For this identifier, no results could be found."
            End Select

            If Not nUrl = "" Then

                nHtml = Http.Html(nUrl)
                nJson = Http.getJson(nHtml)

                nTitle = New Title
                If nTitle.parseJson(nJson) Then title = nTitle

            End If

            Retry = 0

        Catch nException As Exception

            Select Case nException.HResult
                Case -2146233079
                    Retry += 1
                    If Retry < 3 Then title = title(qId)
            End Select

            nException = Nothing

        Finally

            nUrl = Nothing
            nCode = Nothing
            nHtml = Nothing
            nJson = Nothing
            nMode = Nothing
            nTitle = Nothing

            qId = Nothing

        End Try

    End Function

    Public Shared Function name(qId As String) As Name

        name = New Name

        Dim nName As Name = Nothing
        Dim nUrl As String = Nothing
        Dim nHtml As String = Nothing
        Dim nJson As String = Nothing
        Dim nMode As String = Nothing
        Dim nCode As Integer = Nothing

        Try

            nMode = ""
            If qId.Length > 2 Then
                If Integer.TryParse(qId.Substring(2), nCode) Then
                    nMode = qId.Substring(0, 2)
                End If
            End If

            nUrl = ""
            Select Case nMode
                Case "nm"
                    nUrl = Environment.Url & "/name/" & qId & "/"
                Case "tt"
                    name.errorMessage = "For this identifier, no results could be found. Please try searching for this reference using the 'title' method."
                Case Else
                    name.errorMessage = "For this identifier, no results could be found."
            End Select

            If Not nUrl = "" Then

                nHtml = Http.Html(nUrl)
                nJson = Http.getJson(nHtml)

                nName = New Name
                If nName.parseJson(nJson) Then name = nName

            End If

            Retry = 0

        Catch nException As Exception

            Select Case nException.HResult
                Case -2146233079
                    Retry += 1
                    If Retry < 3 Then name = name(qId)
            End Select

            nException = Nothing

        Finally

            nUrl = Nothing
            nCode = Nothing
            nHtml = Nothing
            nJson = Nothing
            nMode = Nothing
            nName = Nothing

            qId = Nothing

        End Try

    End Function

End Class

Public Class IMDb

#Region " Friends "

    Protected pToken As String = Nothing
    Protected pRetry As Integer = Nothing

#End Region

#Region " Private "

    Private Sub Initialize()

        Try

            pToken = ""

        Catch nException As Exception

            nException = Nothing

        End Try

    End Sub

#End Region

#Region " Builders "

    Public Sub New()

        Try

            Me.Initialize()

            pToken = "free"

        Catch nException As Exception

            nException = Nothing

        End Try

    End Sub

    Public Sub New(qToken As String)

        Try

            Me.Initialize()

            pToken = qToken

        Catch nException As Exception

            nException = Nothing

        End Try

    End Sub

#End Region

#Region " Methods "

    Public Function search(qExpression As String, Optional qMode As eSearch = eSearch.All) As Results

        search = New Results

        Dim nUrl As String = Nothing
        Dim nJson As String = Nothing
        Dim nHttp As HttpClient = Nothing
        Dim nRequest As HttpRequestMessage = Nothing
        Dim nResponse As HttpResponseMessage = Nothing

        Try

            nUrl = "https://www.mezil.it/api/imdb/" & pToken & "/search/" & qExpression & "/" & qMode.ToString
            nHttp = New HttpClient
            nRequest = New HttpRequestMessage(HttpMethod.Get, nUrl)
            nRequest.Headers.Add("Accept", "application/json")
            nResponse = nHttp.SendAsync(nRequest).Result

            If nResponse.IsSuccessStatusCode Then
                nJson = nResponse.Content.ReadAsStringAsync().Result
                search = JsonConvert.DeserializeObject(Of Results)(nJson)
            Else
                search.errorMessage = nResponse.StatusCode.ToString
            End If

        Catch nException As Exception

            nException = Nothing

        Finally

            If nHttp IsNot Nothing Then nHttp.Dispose()
            If nRequest IsNot Nothing Then nRequest.Dispose()
            If nResponse IsNot Nothing Then nResponse.Dispose()

            nUrl = Nothing
            nJson = Nothing
            nHttp = Nothing
            nRequest = Nothing
            nResponse = Nothing

            qMode = Nothing
            qExpression = Nothing

        End Try

    End Function

    Public Function title(qId As String) As Title

        title = New Title

        Dim nUrl As String = Nothing
        Dim nJson As String = Nothing
        Dim nHttp As HttpClient = Nothing
        Dim nRequest As HttpRequestMessage = Nothing
        Dim nResponse As HttpResponseMessage = Nothing

        Try

            nUrl = "https://www.mezil.it/api/imdb/" & pToken & "/title/" & qId
            nHttp = New HttpClient
            nRequest = New HttpRequestMessage(HttpMethod.Get, nUrl)
            nRequest.Headers.Add("Accept", "application/json")
            nResponse = nHttp.SendAsync(nRequest).Result

            If nResponse.IsSuccessStatusCode Then
                nJson = nResponse.Content.ReadAsStringAsync().Result
                title = JsonConvert.DeserializeObject(Of Title)(nJson)
            Else
                title.errorMessage = nResponse.StatusCode.ToString
            End If

        Catch nException As Exception

            nException = Nothing

        Finally

            If nHttp IsNot Nothing Then nHttp.Dispose()
            If nRequest IsNot Nothing Then nRequest.Dispose()
            If nResponse IsNot Nothing Then nResponse.Dispose()

            nUrl = Nothing
            nJson = Nothing
            nHttp = Nothing
            nRequest = Nothing
            nResponse = Nothing

            qId = Nothing

        End Try

    End Function

    Public Function name(qId As String) As Name

        name = New Name

        Dim nUrl As String = Nothing
        Dim nJson As String = Nothing
        Dim nHttp As HttpClient = Nothing
        Dim nRequest As HttpRequestMessage = Nothing
        Dim nResponse As HttpResponseMessage = Nothing

        Try

            nUrl = "https://www.mezil.it/api/imdb/" & pToken & "/name/" & qId
            nHttp = New HttpClient
            nRequest = New HttpRequestMessage(HttpMethod.Get, nUrl)
            nRequest.Headers.Add("Accept", "application/json")
            nResponse = nHttp.SendAsync(nRequest).Result

            If nResponse.IsSuccessStatusCode Then
                nJson = nResponse.Content.ReadAsStringAsync().Result
                name = JsonConvert.DeserializeObject(Of Name)(nJson)
            Else
                name.errorMessage = nResponse.StatusCode.ToString
            End If

        Catch nException As Exception

            nException = Nothing

        Finally

            If nHttp IsNot Nothing Then nHttp.Dispose()
            If nRequest IsNot Nothing Then nRequest.Dispose()
            If nResponse IsNot Nothing Then nResponse.Dispose()

            nUrl = Nothing
            nJson = Nothing
            nHttp = Nothing
            nRequest = Nothing
            nResponse = Nothing

            qId = Nothing

        End Try

    End Function

#End Region

#Region " Property "

    ReadOnly Property Plan As String

        Get

            Plan = ""

            Try

                Plan = "Free"

            Catch nException As Exception

                nException = Nothing

            End Try

        End Get

    End Property

#End Region

End Class

Public Class Results

#Region " Builders "

    Public Sub New()

        Try

            Me.Initialize()

        Catch nException As Exception

            nException = Nothing

        End Try

    End Sub

    Public Sub New(qJson As String)

        Try

            Me.Initialize()

            Me.parseJson(qJson)

        Catch nException As Exception

            nException = Nothing

        Finally

            qJson = Nothing

        End Try

    End Sub

#End Region

#Region " Private "

    Private Sub Initialize()

        Try

            Me.searchTerm = ""
            Me.searchType = ""
            Me.titles = New List(Of TitleResult)
            Me.celebs = New List(Of CelebResult)
            Me.companies = New List(Of CompanyResult)
            Me.keywords = New List(Of KeywordResult)
            Me.errorMessage = ""

        Catch nException As Exception

            nException = Nothing

        End Try

    End Sub

#End Region

#Region " Methods "

    Public Overrides Function ToString() As String

        ToString = "<Results>"

        Try

            If Me.searchType = "" Then
                ToString = "Search " & Me.searchTerm
            Else
                ToString = "Search " & Me.searchTerm & " [" & Me.searchType & "]"
            End If

        Catch nException As Exception

            nException = Nothing

        End Try

    End Function

    Public Function parseJson(qJson As String) As Boolean

        parseJson = False

        Dim nBrowser As Browser = Nothing

        Try

            nBrowser = New Browser(qJson)
            parseJson = parseJson(nBrowser)

        Catch nException As Exception

            nException = Nothing

        Finally

            nBrowser = Nothing

            qJson = Nothing

        End Try

    End Function

    Public Function parseJson(qBrowser As Browser) As Boolean

        parseJson = False

        Dim nBrowser As Browser = Nothing
        Dim nCeleb As CelebResult = Nothing
        Dim nTitle As TitleResult = Nothing
        Dim nCompany As CompanyResult = Nothing
        Dim nKeyword As KeywordResult = Nothing

        Try

            If qBrowser.Seek("props\pageProps\findPageMeta\searchTerm") Then Me.searchTerm = qBrowser.Value
            If qBrowser.Seek("props\pageProps\findPageMeta\searchType") Then Me.searchType = qBrowser.Value

            If qBrowser.Seek("props\pageProps\nameResults\results") Then
                For Each nBrowser In qBrowser.List
                    If CelebResult.parseJson(nBrowser, nCeleb) Then Me.celebs.Add(nCeleb)
                Next
            End If

            If qBrowser.Seek("props\pageProps\titleResults\results") Then
                For Each nBrowser In qBrowser.List
                    If TitleResult.parseJson(nBrowser, nTitle) Then Me.titles.Add(nTitle)
                Next
            End If

            If qBrowser.Seek("props\pageProps\companyResults\results") Then
                For Each nBrowser In qBrowser.List
                    If CompanyResult.parseJson(nBrowser, nCompany) Then Me.companies.Add(nCompany)
                Next
            End If

            If qBrowser.Seek("props\pageProps\keywordResults\results") Then
                For Each nBrowser In qBrowser.List
                    If KeywordResult.parseJson(nBrowser, nKeyword) Then Me.keywords.Add(nKeyword)
                Next
            End If

            parseJson = True

        Catch nException As Exception

            nException = Nothing

        Finally

            nCeleb = Nothing
            nTitle = Nothing
            nCompany = Nothing
            nBrowser = Nothing
            nKeyword = Nothing

            qBrowser = Nothing

        End Try

    End Function

    Public Function json() As String

        json = ""

        Try

            json = Newtonsoft.Json.JsonConvert.SerializeObject(Me)

        Catch nException As Exception

            nException = Nothing

        End Try

    End Function

#End Region

#Region " Property "

    Public Property searchTerm As String = Nothing
    Public Property searchType As String = Nothing
    Public Property titles As List(Of TitleResult) = Nothing
    Public Property celebs As List(Of CelebResult) = Nothing
    Public Property companies As List(Of CompanyResult) = Nothing
    Public Property keywords As List(Of KeywordResult) = Nothing
    Public Property errorMessage As String = Nothing

#End Region

End Class

Public Class TitleResult

#Region " Shared "

    Public Shared Function parseJson(qBrowser As Browser, ByRef qTitleResult As TitleResult) As Boolean

        parseJson = False
        qTitleResult = New TitleResult

        Try

            parseJson = qTitleResult.parseJson(qBrowser)

        Catch nException As Exception

            nException = Nothing

        Finally

            qBrowser = Nothing

        End Try

    End Function

#End Region

#Region " Builders "

    Public Sub New()

        Try

            Me.Initialize()

        Catch nException As Exception

            nException = Nothing

        End Try

    End Sub

#End Region

#Region " Private "

    Private Sub Initialize()

        Try

            Me.id = ""
            Me.titleNameText = ""
            Me.titleReleaseText = ""
            Me.titleTypeText = ""
            Me.imageType = ""
            Me.topCredits = New List(Of String)
            Me.poster = New Photo
            Me.series = New Series

        Catch nException As Exception

            nException = Nothing

        End Try

    End Sub

#End Region

#Region " Methods "

    Public Overrides Function ToString() As String

        ToString = "<TitleResult>"

        Try

            ToString = Me.titleNameText & " (" & Me.titleReleaseText & ") [" & Me.imageType & "]"

        Catch nException As Exception

            nException = Nothing

        End Try

    End Function

    Public Function parseJson(qBrowser As Browser) As Boolean

        parseJson = False

        Try

            If qBrowser.Seek("id") Then Me.id = qBrowser.Value
            If qBrowser.Seek("titleNameText") Then Me.titleNameText = qBrowser.Value
            If qBrowser.Seek("titleReleaseText") Then Me.titleReleaseText = qBrowser.Value
            If qBrowser.Seek("titleTypeText") Then Me.titleTypeText = qBrowser.Value
            If qBrowser.Seek("imageType") Then Me.imageType = qBrowser.Value
            If qBrowser.Seek("titlePosterImageModel") Then Me.poster.parseJson(qBrowser.Truncate)
            If qBrowser.Seek("topCredits") Then Me.topCredits = qBrowser.Values

            Me.series.parseJson(qBrowser)

            parseJson = True

        Catch nException As Exception

            nException = Nothing

        Finally

            qBrowser = Nothing

        End Try

    End Function

#End Region

#Region " Property "

    Public Property id As String = Nothing
    Public Property titleNameText As String = Nothing
    Public Property titleReleaseText As String = Nothing
    Public Property titleTypeText As String = Nothing
    Public Property imageType As String = Nothing
    Public Property topCredits As List(Of String) = Nothing
    Public Property poster As Photo = Nothing
    Public Property series As Series = Nothing

#End Region

End Class

Public Class CelebResult

#Region " Shared "

    Public Shared Function parseJson(qBrowser As Browser, ByRef qCelebResult As CelebResult) As Boolean

        parseJson = False
        qCelebResult = New CelebResult

        Try

            parseJson = qCelebResult.parseJson(qBrowser)

        Catch nException As Exception

            nException = Nothing

        Finally

            qBrowser = Nothing

        End Try

    End Function

#End Region

#Region " Builders "

    Public Sub New()

        Try

            Me.Initialize()

        Catch nException As Exception

            nException = Nothing

        End Try

    End Sub

#End Region

#Region " Private "

    Private Sub Initialize()

        Try

            Me.id = ""
            Me.displayNameText = ""
            Me.knownForTitleText = ""
            Me.knownForTitleYear = ""
            Me.knownForJobCategory = ""
            Me.akaName = ""
            Me.avatar = New Photo

        Catch nException As Exception

            nException = Nothing

        End Try

    End Sub

#End Region

#Region " Methods "

    Public Overrides Function ToString() As String

        ToString = "<CelebResult>"

        Try

            ToString = Me.displayNameText

        Catch nException As Exception

            nException = Nothing

        End Try

    End Function

    Public Function parseJson(qJson As String) As Boolean

        parseJson = False

        Dim nBrowser As Browser = Nothing

        Try

            nBrowser = New Browser(qJson)
            parseJson = parseJson(nBrowser)

        Catch nException As Exception

            nException = Nothing

        Finally

            nBrowser = Nothing

            qJson = Nothing

        End Try

    End Function

    Public Function parseJson(qBrowser As Browser) As Boolean

        parseJson = False

        Try

            If qBrowser.Seek("id") Then Me.id = qBrowser.Value
            If qBrowser.Seek("displayNameText") Then Me.displayNameText = qBrowser.Value
            If qBrowser.Seek("knownForTitleText") Then Me.knownForTitleText = qBrowser.Value
            If qBrowser.Seek("knownForTitleYear") Then Me.knownForTitleYear = qBrowser.Value
            If qBrowser.Seek("knownForJobCategory") Then Me.knownForJobCategory = qBrowser.Value
            If qBrowser.Seek("avatarImageModel\url") Then Me.avatar.url = qBrowser.Value
            If qBrowser.Seek("avatarImageModel\maxWidth") Then Me.avatar.width = qBrowser.Value
            If qBrowser.Seek("avatarImageModel\maxHeight") Then Me.avatar.height = qBrowser.Value
            If qBrowser.Seek("avatarImageModel\caption") Then Me.avatar.caption = qBrowser.Value

            parseJson = True

        Catch nException As Exception

            nException = Nothing

        Finally

            qBrowser = Nothing

        End Try

    End Function

#End Region

#Region " Property "

    Public Property id As String = Nothing
    Public Property displayNameText As String = Nothing
    Public Property knownForTitleText As String = Nothing
    Public Property knownForTitleYear As String = Nothing
    Public Property knownForJobCategory As String = Nothing
    Public Property avatar As Photo = Nothing
    Public Property akaName As String = Nothing

#End Region

End Class

Public Class CompanyResult

#Region " Shared "

    Public Shared Function parseJson(qBrowser As Browser, ByRef qCompanyResult As CompanyResult) As Boolean

        parseJson = False
        qCompanyResult = New CompanyResult

        Try

            parseJson = qCompanyResult.parseJson(qBrowser)

        Catch nException As Exception

            nException = Nothing

        Finally

            qBrowser = Nothing

        End Try

    End Function

#End Region

#Region " Builders "

    Public Sub New()

        Try

            Me.Initialize()

        Catch nException As Exception

            nException = Nothing

        End Try

    End Sub

#End Region

#Region " Private "

    Private Sub Initialize()

        Try

            Me.id = ""
            Me.name = ""
            Me.country = ""
            Me.type = ""

        Catch nException As Exception

            nException = Nothing

        End Try

    End Sub

#End Region

#Region " Methods "

    Public Overrides Function ToString() As String

        ToString = "<CompanyResult>"

        Try

            If Me.country = "" Then
                ToString = Me.name
            Else
                ToString = Me.name & " (" & Me.country & ")"
            End If

        Catch nException As Exception

            nException = Nothing

        End Try

    End Function

    Public Function parseJson(qBrowser As Browser) As Boolean

        parseJson = False

        Try

            If qBrowser.Seek("id") Then Me.id = qBrowser.Value
            If qBrowser.Seek("companyName") Then Me.name = qBrowser.Value
            If qBrowser.Seek("countryText") Then Me.country = qBrowser.Value
            If qBrowser.Seek("typeText") Then Me.type = qBrowser.Value

            parseJson = True

        Catch nException As Exception

            nException = Nothing

        Finally

            qBrowser = Nothing

        End Try

    End Function

#End Region

#Region " Property "

    Public Property id As String = Nothing
    Public Property name As String = Nothing
    Public Property country As String = Nothing
    Public Property type As String = Nothing

#End Region

End Class

Public Class KeywordResult

#Region " Shared "

    Public Shared Function parseJson(qBrowser As Browser, ByRef qKeywordResult As KeywordResult) As Boolean

        parseJson = False
        qKeywordResult = New KeywordResult

        Try

            parseJson = qKeywordResult.parseJson(qBrowser)

        Catch nException As Exception

            nException = Nothing

        Finally

            qBrowser = Nothing

        End Try

    End Function

#End Region

#Region " Builders "

    Public Sub New()

        Try

            Me.Initialize()

        Catch nException As Exception

            nException = Nothing

        End Try

    End Sub

#End Region

#Region " Private "

    Private Sub Initialize()

        Try

            Me.id = ""
            Me.keyword = ""
            Me.titles = ""

        Catch nException As Exception

            nException = Nothing

        End Try

    End Sub

#End Region

#Region " Methods "

    Public Overrides Function ToString() As String

        ToString = "<KeywordResult>"

        Try

            If Not Me.id = "" Then ToString = Me.keyword & " (" & Me.titles & ")"

        Catch nException As Exception

            nException = Nothing

        End Try

    End Function

    Public Function parseJson(qBrowser As Browser) As Boolean

        parseJson = False

        Try

            If qBrowser.Seek("id") Then Me.id = qBrowser.Value
            If qBrowser.Seek("keywordText") Then Me.keyword = qBrowser.Value
            If qBrowser.Seek("numTitles") Then Me.titles = qBrowser.Value

            parseJson = True

        Catch nException As Exception

            nException = Nothing

        Finally

            qBrowser = Nothing

        End Try

    End Function

#End Region

#Region " Property "

    Public Property id As String = Nothing
    Public Property keyword As String = Nothing
    Public Property titles As String = Nothing

#End Region

End Class

Public Class Series

#Region " Shared "

    Public Shared Function parseJson(qBrowser As Browser, ByRef qTitleResult As TitleResult) As Boolean

        parseJson = False
        qTitleResult = New TitleResult

        Try

            parseJson = qTitleResult.parseJson(qBrowser)

        Catch nException As Exception

            nException = Nothing

        Finally

            qBrowser = Nothing

        End Try

    End Function

#End Region

#Region " Builders "

    Public Sub New()

        Try

            Me.Initialize()

        Catch nException As Exception

            nException = Nothing

        End Try

    End Sub

#End Region

#Region " Private "

    Private Sub Initialize()

        Try

            Me.id = ""
            Me.name = ""
            Me.release = ""
            Me.type = ""
            Me.season = ""
            Me.episode = ""

        Catch nException As Exception

            nException = Nothing

        End Try

    End Sub

#End Region

#Region " Methods "

    Public Overrides Function ToString() As String

        ToString = "<Series>"

        Try

            If Me.id = "" Then
                ToString = "<empty>"
            Else
                ToString = Me.name & " Season " & Me.season & " Episode: " & Me.episode & " (" & Me.release & ")"
            End If

        Catch nException As Exception

            nException = Nothing

        End Try

    End Function

    Public Function parseJson(qBrowser As Browser) As Boolean

        parseJson = False

        Try

            If qBrowser.Seek("seriesId") Then Me.id = qBrowser.Value
            If qBrowser.Seek("seriesNameText") Then Me.name = qBrowser.Value
            If qBrowser.Seek("seriesReleaseText") Then Me.release = qBrowser.Value
            If qBrowser.Seek("seriesTypeText") Then Me.type = qBrowser.Value
            If qBrowser.Seek("seriesSeasonText") Then Me.season = qBrowser.Value
            If qBrowser.Seek("seriesEpisodeText") Then Me.episode = qBrowser.Value

            parseJson = True

        Catch nException As Exception

            nException = Nothing

        Finally

            qBrowser = Nothing

        End Try

    End Function

#End Region

#Region " Property "

    Public Property id As String = Nothing
    Public Property name As String = Nothing
    Public Property release As String = Nothing
    Public Property type As String = Nothing
    Public Property season As String = Nothing
    Public Property episode As String = Nothing

#End Region

End Class

Public Class Photo

#Region " Shared "

    Public Shared Function parseImageConnection(qJObject As JObject) As List(Of Photo)

        parseImageConnection = New List(Of Photo)

        Dim nPhoto As Photo = Nothing
        Dim nJObject As JObject = Nothing
        Dim nList As List(Of Photo) = Nothing

        Try

            nList = New List(Of Photo)
            If qJObject IsNot Nothing AndAlso qJObject("__typename") IsNot Nothing AndAlso qJObject("__typename").ToString = "ImageConnection" Then
                If qJObject("edges") IsNot Nothing Then
                    For Each nJObject In qJObject("edges")
                        If nJObject("node") IsNot Nothing AndAlso Photo.parse(nJObject("node"), nPhoto) Then
                            nList.Add(nPhoto)
                        End If
                    Next
                End If
            End If
            parseImageConnection = nList

        Catch nException As Exception

            nException = Nothing

        Finally

            nList = Nothing
            nPhoto = Nothing
            nJObject = Nothing

            qJObject = Nothing

        End Try

    End Function

    Public Shared Function parse(qJObject As JObject, ByRef qPhoto As Photo) As Boolean

        parse = False
        qPhoto = New Photo

        Try

            parse = qPhoto.parseJson(qJObject)

        Catch nException As Exception

            nException = Nothing

        Finally

            qJObject = Nothing

        End Try

    End Function

    Public Shared Function parseJsonToList(qBrowser As Browser) As List(Of Photo)

        parseJsonToList = New List(Of Photo)

        Dim nPhoto As Photo = Nothing
        Dim nBrowser As Browser = Nothing
        Dim nList As List(Of Photo) = Nothing

        Try

            nList = New List(Of Photo)

            If qBrowser.Seek("__typename") Then
                Select Case qBrowser.Value
                    Case "ImageConnection"
                        qBrowser.Seek("edges")
                End Select
            End If

            For Each nBrowser In qBrowser.List
                If nBrowser.Seek("__typename") Then
                    Select Case nBrowser.Value
                        Case "ImageEdge"
                            nPhoto = New Photo
                            If nBrowser.Seek("node\id") Then nPhoto.id = nBrowser.Value
                            If nBrowser.Seek("node\url") Then nPhoto.url = nBrowser.Value
                            If nBrowser.Seek("node\width") Then nPhoto.width = nBrowser.Value
                            If nBrowser.Seek("node\height") Then nPhoto.height = nBrowser.Value
                            If nBrowser.Seek("node\caption\plainText") Then nPhoto.caption = nBrowser.Value
                            nList.Add(nPhoto)
                    End Select
                End If
            Next

            parseJsonToList = nList

        Catch nException As Exception

            nException = Nothing

        Finally

            nList = Nothing
            nPhoto = Nothing
            nBrowser = Nothing

        End Try

    End Function

#End Region

#Region " Builders "

    Public Sub New()

        Try

            Me.Initialize()

        Catch nException As Exception

            nException = Nothing

        End Try

    End Sub

#End Region

#Region " Private "

    Private Sub Initialize()

        Try

            Me.id = ""
            Me.url = ""
            Me.height = 0
            Me.width = 0
            Me.caption = ""

        Catch nException As Exception

            nException = Nothing

        End Try

    End Sub

#End Region

#Region " Methods "

    Public Overrides Function ToString() As String

        ToString = "<Photo>"

        Try

            If Me.url = "" Then
                ToString = "<Empty>"
            ElseIf Not Me.caption = "" Then
                ToString = Me.caption
            ElseIf Not Me.width = "" AndAlso Not Me.height = "" Then
                ToString = Me.width & "x" & Me.height
            Else
                ToString = "<Full>"
            End If

        Catch nException As Exception

            nException = Nothing

        End Try

    End Function

    Public Function parseJson(qJObject As JObject) As Boolean

        parseJson = False

        Try

            If qJObject IsNot Nothing AndAlso qJObject("__typename") IsNot Nothing Then
                Select Case qJObject("__typename").ToString
                    Case "Image", "ImageEdge", "Thumbnail"
                        If qJObject("id") IsNot Nothing Then Me.id = qJObject("id").ToString
                        If qJObject("url") IsNot Nothing Then Me.url = qJObject("url").ToString
                        If qJObject("width") IsNot Nothing Then Me.width = qJObject("width").ToString
                        If qJObject("height") IsNot Nothing Then Me.height = qJObject("height").ToString
                        If qJObject("caption") IsNot Nothing AndAlso qJObject("caption")("plainText") IsNot Nothing Then Me.caption = qJObject("caption")("plainText").ToString
                        parseJson = True
                End Select
            End If

        Catch nException As Exception

            nException = Nothing

        Finally

            qJObject = Nothing

        End Try

    End Function

    Public Function parseJson(qBrowser As Browser) As Boolean

        parseJson = False

        Try

            If qBrowser.Seek("__typename") Then
                Select Case qBrowser.Value
                    Case "Image", "ImageEdge", "Thumbnail", "Markdown"
                        If qBrowser.Seek("node") Then qBrowser = qBrowser.Truncate
                        If qBrowser.Seek("id") Then Me.id = qBrowser.Value
                        If qBrowser.Seek("url") Then Me.url = qBrowser.Value
                        If qBrowser.Seek("width") Then Me.width = qBrowser.Value
                        If qBrowser.Seek("height") Then Me.height = qBrowser.Value
                        If qBrowser.Seek("caption\plainText") Then Me.caption = qBrowser.Value
                        parseJson = True
                    Case Else

                End Select
            Else
                If qBrowser.Seek("url") Then Me.url = qBrowser.Value
                If qBrowser.Seek("maxWidth") Then Me.width = qBrowser.Value
                If qBrowser.Seek("maxHeight") Then Me.height = qBrowser.Value
                If qBrowser.Seek("caption") Then Me.caption = qBrowser.Value
            End If

        Catch nException As Exception

            nException = Nothing

        Finally

            qBrowser = Nothing

        End Try

    End Function

#End Region

#Region " Property "

    Public Property id As String = Nothing
    Public Property url As String = Nothing
    Public Property width As String = Nothing
    Public Property height As String = Nothing
    Public Property caption As String = Nothing

#End Region

End Class

Public Class Video

#Region " Shared "

    Public Shared Function parseJson(qBrowser As Browser, ByRef qVideo As Video) As Boolean

        parseJson = False
        qVideo = New Video

        Dim nBrowser As Browser = Nothing
        Dim nPlaybackURL As PlaybackURL = Nothing

        Try

            If qBrowser.Seek("__typename") Then
                Select Case qBrowser.Value
                    Case "VideoEdge"
                        If qBrowser.Seek("node") Then
                            qBrowser = qBrowser.Truncate

                            If qBrowser.Seek("id") Then qVideo.id = qBrowser.Value
                            If qBrowser.Seek("runtime\value") Then qVideo.runtime.fromSeconds(qBrowser.Value)
                            If qBrowser.Seek("name\value") Then qVideo.name = qBrowser.Value
                            If qBrowser.Seek("description\value") Then qVideo.description = qBrowser.Value
                            If qBrowser.Seek("thumbnail") Then qVideo.thumbnail.parseJson(qBrowser.Truncate)
                            If qBrowser.Seek("contentType\displayName\value") Then qVideo.type = qBrowser.Value
                            If qBrowser.Seek("playbackURLs") Then
                                For Each nBrowser In qBrowser.List
                                    nPlaybackURL = New PlaybackURL
                                    If nPlaybackURL.parseJson(nBrowser) Then qVideo.playbackURLs.Add(nPlaybackURL)
                                Next
                            End If

                            parseJson = True

                        End If
                End Select
            End If

        Catch nException As Exception

            nException = Nothing

        Finally

            nBrowser = Nothing
            nPlaybackURL = Nothing

            qBrowser = Nothing

        End Try

    End Function

    Public Shared Function parseJsonToList(qBrowser As Browser) As List(Of Video)

        parseJsonToList = New List(Of Video)

        Dim nVideo As Video = Nothing
        Dim nBrowser As Browser = Nothing
        Dim nList As List(Of Video) = Nothing

        Try

            nList = New List(Of Video)

            If qBrowser.Seek("__typename") Then
                Select Case qBrowser.Value
                    Case "VideoConnection"
                        qBrowser.Seek("edges")
                End Select
            End If

            For Each nBrowser In qBrowser.List
                If parseJson(nBrowser, nVideo) Then nList.Add(nVideo)
            Next

            parseJsonToList = nList

        Catch nException As Exception

            nException = Nothing

        Finally

            nList = Nothing
            nVideo = Nothing
            nBrowser = Nothing

        End Try

    End Function

#End Region

#Region " Builders "

    Public Sub New()

        Try

            Me.Initialize()

        Catch nException As Exception

            nException = Nothing

        End Try

    End Sub

#End Region

#Region " Private "

    Private Sub Initialize()

        Try

            Me.id = ""
            Me.runtime = New Runtime
            Me.name = ""
            Me.description = ""
            Me.thumbnail = New Photo
            Me.type = ""
            Me.playbackURLs = New List(Of PlaybackURL)

        Catch nException As Exception

            nException = Nothing

        End Try

    End Sub

#End Region

#Region " Methods "

    Public Overrides Function ToString() As String

        ToString = "<Video>"

        Dim nResult As String = Nothing

        Try

            nResult = ""
            If Not Me.name = "" Then nResult = Me.name
            If Not Me.runtime.plainText = "" Then nResult = nResult & " (" & Me.runtime.plainText & ")"
            ToString = nResult

        Catch nException As Exception

            nException = Nothing

        Finally

            nResult = Nothing

        End Try

    End Function

    Public Function parseJson(qBrowser As Browser) As Boolean

        parseJson = False

        Dim nBrowser As Browser = Nothing
        Dim nPlaybackURL As PlaybackURL = Nothing

        Try

            If qBrowser.Seek("__typename") Then
                Select Case qBrowser.Value
                    Case "Video", "VideoEdge"
                        If qBrowser.Seek("node\id") Then Me.id = qBrowser.Value
                        If qBrowser.Seek("node\runtime\value") Then Me.runtime.fromSeconds(qBrowser.Value)
                        If qBrowser.Seek("node\name\value") Then Me.name = qBrowser.Value
                        If qBrowser.Seek("node\description\value") Then Me.description = qBrowser.Value
                        If qBrowser.Seek("node\thumbnail") Then Me.thumbnail.parseJson(qBrowser.Truncate)
                        If qBrowser.Seek("node\contentType\displayName\value") Then Me.type = qBrowser.Value
                        If qBrowser.Seek("node\playbackURLs") Then
                            For Each nBrowser In qBrowser.List
                                nPlaybackURL = New PlaybackURL
                                If nPlaybackURL.parseJson(nBrowser) Then Me.playbackURLs.Add(nPlaybackURL)
                            Next
                        End If
                        parseJson = True
                End Select
            End If

        Catch nException As Exception

            nException = Nothing

        Finally

            nBrowser = Nothing
            nPlaybackURL = Nothing

            qBrowser = Nothing

        End Try

    End Function

#End Region

#Region " Property "

    Public Property id As String = Nothing
    Public Property runtime As Runtime = Nothing
    Public Property name As String = Nothing
    Public Property description As String = Nothing
    Public Property thumbnail As Photo = Nothing
    Public Property type As String = Nothing
    Public Property playbackURLs As List(Of PlaybackURL) = Nothing

#End Region

End Class

Public Class Http

    Public Shared Function Html(qUrl As String) As String

        Html = ""

        Dim nHttpClient As HttpClient = Nothing
        Dim nResult As Task(Of String) = Nothing
        Dim nResponse As HttpResponseMessage = Nothing
        Dim nTask As Task(Of HttpResponseMessage) = Nothing

        Try

            nHttpClient = New HttpClient

            nTask = nHttpClient.PostAsync(qUrl, Nothing)

            If nTask.Wait(3000) AndAlso nTask.Result.IsSuccessStatusCode Then
                nResult = nTask.Result.Content.ReadAsStringAsync
                If nResult.Wait(3000) AndAlso nResult.IsCompleted Then
                    Html = nResult.Result
                End If
            End If

        Catch nException As Exception

            nException = Nothing

        Finally

            If nTask IsNot Nothing Then nTask.Dispose()
            If nResult IsNot Nothing Then nResult.Dispose()
            'If nContent IsNot Nothing Then nContent.Dispose()
            If nResponse IsNot Nothing Then nResponse.Dispose()
            If nHttpClient IsNot Nothing Then nHttpClient.Dispose()

            nTask = Nothing
            nResult = Nothing
            'nContent = Nothing
            nResponse = Nothing
            nHttpClient = Nothing

            qUrl = Nothing
            'qParameters = Nothing

        End Try

    End Function

    Public Shared Function getJson(qHtml As String) As String

        getJson = ""

        Dim nNode As HtmlNode = Nothing
        Dim nDocument As HtmlDocument = Nothing
        Dim nCollection As HtmlNodeCollection = Nothing

        Try

            nDocument = New HtmlDocument
            nDocument.LoadHtml(qHtml)

            nCollection = nDocument.DocumentNode.SelectNodes("//script[@type='application/json']")

            If nCollection IsNot Nothing Then
                For Each nNode In nCollection
                    getJson = nNode.InnerText
                    Exit For
                Next
            End If

        Catch nException As Exception

            nException = Nothing

        Finally

            nNode = Nothing
            nDocument = Nothing
            nCollection = Nothing

            qHtml = Nothing

        End Try

    End Function

End Class

Public Class Title

#Region " Shared "

    Public Shared Function parseTitleList(qJObject As JObject) As List(Of Title)

        parseTitleList = New List(Of Title)

        Dim nTitle As Title = Nothing
        Dim nJObject As JObject = Nothing
        Dim nList As List(Of Title) = Nothing

        Try

            nList = New List(Of Title)
            If qJObject("__typename") IsNot Nothing Then
                Select Case qJObject("__typename").ToString
                    Case "MoreLikeThisConnection"
                        If qJObject("edges") IsNot Nothing Then
                            For Each nJObject In qJObject("edges")
                                nTitle = New Title
                                If nTitle.parseJson(nJObject) Then
                                    nList.Add(nTitle)
                                End If
                            Next
                        End If
                End Select
            End If
            parseTitleList = nList

        Catch nException As Exception

            nException = Nothing

        Finally

            nList = Nothing
            nTitle = Nothing
            nJObject = Nothing

            qJObject = Nothing

        End Try

    End Function

    Public Shared Function parseTitleList(qBrowser As Browser) As List(Of Title)

        parseTitleList = New List(Of Title)

        Dim nTitle As Title = Nothing
        Dim nBrowser As Browser = Nothing
        Dim nList As List(Of Title) = Nothing

        Try

            nList = New List(Of Title)
            If qBrowser.Seek("__typename") Then
                Select Case qBrowser.Value
                    Case "MoreLikeThisConnection"
                        If qBrowser.Seek("edges") Then
                            For Each nBrowser In qBrowser.List
                                nTitle = New Title
                                If nTitle.parseJson(nBrowser) Then
                                    nList.Add(nTitle)
                                End If
                            Next
                        End If
                End Select
            End If
            parseTitleList = nList

        Catch nException As Exception

            nException = Nothing

        Finally

            nList = Nothing
            nTitle = Nothing
            nBrowser = Nothing

            qBrowser = Nothing

        End Try

    End Function

#End Region

#Region " Builders "

    Public Sub New()

        Try

            Me.Initialize()

        Catch nException As Exception

            nException = Nothing

        End Try

    End Sub

#End Region

#Region " Private "

    Private Sub Initialize()

        Try

            Me.id = ""
            Me.title = ""
            Me.originalTitle = ""
            Me.type = ""
            Me.year = ""
            Me.image = New Photo
            Me.releaseDate = ""
            Me.runtime = New Runtime
            Me.plot = ""
            Me.awards = New Awards
            Me.directors = New List(Of Person)
            Me.writers = New List(Of Person)
            Me.actors = New List(Of Person)
            Me.genres = New List(Of Genre)
            Me.companies = New List(Of Company)
            Me.countries = New List(Of Country)
            Me.languages = New List(Of Language)
            Me.contentRating = ""
            Me.rating = New Rating
            Me.metascore = ""
            Me.photos = New List(Of Photo)
            Me.videos = New List(Of Video)
            Me.boxOffice = New BoxOffice
            Me.keywords = New List(Of String)
            Me.similars = New List(Of Title)
            Me.episodes = New Episodes
            Me.errorMessage = ""

        Catch nException As Exception

            nException = Nothing

        End Try

    End Sub

#End Region

#Region " Methods "

    Public Overrides Function ToString() As String

        ToString = "<Title>"

        Try

            ToString = Me.title

        Catch nException As Exception

            nException = Nothing

        End Try

    End Function

    Public Function parseJson(qJson As String) As Boolean

        parseJson = False

        Dim nJObject As JObject = Nothing

        Try

            nJObject = JObject.Parse(qJson)
            parseJson = parseJson(nJObject)

        Catch nException As Exception

            nException = Nothing

        Finally

            nJObject = Nothing

            qJson = Nothing

        End Try

    End Function

    Public Function parseJson(qJObject As JObject) As Boolean

        parseJson = False

        Dim nBrowser As Browser = Nothing

        Try

            nBrowser = New Browser(qJObject)
            parseJson = parseJson(nBrowser)

        Catch nException As Exception

            nException = Nothing

        Finally

            nBrowser = Nothing

            qJObject = Nothing

        End Try

    End Function

    Public Function parseJson(qBrowser As Browser) As Boolean

        parseJson = False

        Try

            If qBrowser.Seek("__typename") Then
                Select Case qBrowser.Value
                    Case "MoreLikeThisEdge", "Title"

                        If qBrowser.Seek("node") Then qBrowser = qBrowser.Truncate
                        If qBrowser.Seek("id") Then Me.id = qBrowser.Value
                        If qBrowser.Seek("titleText\text") Then Me.title = qBrowser.Value
                        If qBrowser.Seek("originalTitleText\text") Then Me.originalTitle = qBrowser.Value
                        If qBrowser.Seek("titleType\text") Then Me.type = qBrowser.Value
                        If qBrowser.Seek("releaseYear\year") Then Me.year = qBrowser.Value
                        If qBrowser.Seek("primaryImage") Then Me.image.parseJson(qBrowser.Truncate)
                        If qBrowser.Seek("runtime\seconds") Then Me.runtime.fromSeconds(qBrowser.Value)
                        If qBrowser.Seek("titleGenres") Then Me.genres = Genre.parseJsonToList(qBrowser.Truncate)
                        If qBrowser.Seek("certificate\rating") Then Me.contentRating = qBrowser.Value
                        If qBrowser.Seek("ratingsSummary\aggregateRating") Then Me.rating.aggregateRating = qBrowser.Value
                        If qBrowser.Seek("ratingsSummary\voteCount") Then Me.rating.voteCount = qBrowser.Value

                        parseJson = True

                End Select
            Else

                If qBrowser.Seek("props\pageProps\aboveTheFoldData\id") Then Me.id = qBrowser.Value
                If qBrowser.Seek("props\pageProps\aboveTheFoldData\titleText\text") Then Me.title = qBrowser.Value
                If qBrowser.Seek("props\pageProps\aboveTheFoldData\originalTitleText\text") Then Me.originalTitle = qBrowser.Value
                If qBrowser.Seek("props\pageProps\aboveTheFoldData\titleType\text") Then Me.type = qBrowser.Value
                If qBrowser.Seek("props\pageProps\aboveTheFoldData\releaseYear\year") Then Me.year = qBrowser.Value
                If qBrowser.Seek("props\pageProps\aboveTheFoldData\primaryImage") Then Me.image.parseJson(qBrowser.Truncate)
                If qBrowser.Seek("props\pageProps\aboveTheFoldData\releaseDate\day") Then Me.releaseDate = qBrowser.Value
                If qBrowser.Seek("props\pageProps\aboveTheFoldData\releaseDate\month") Then Me.releaseDate = Me.releaseDate & "/" & qBrowser.Value
                If qBrowser.Seek("props\pageProps\aboveTheFoldData\releaseDate\year") Then Me.releaseDate = Me.releaseDate & "/" & qBrowser.Value
                If qBrowser.Seek("props\pageProps\aboveTheFoldData\runtime\seconds") Then Me.runtime.fromSeconds(qBrowser.Value)
                If qBrowser.Seek("props\pageProps\aboveTheFoldData\plot\plotText\plainText") Then Me.plot = qBrowser.Value
                If qBrowser.Seek("props\pageProps\mainColumnData\wins\total") Then Me.awards.wins = qBrowser.Value
                If qBrowser.Seek("props\pageProps\mainColumnData\nominations\total") Then Me.awards.nomination = qBrowser.Value
                If qBrowser.Seek("props\pageProps\mainColumnData\prestigiousAwardSummary\wins") Then Me.awards.awardWins = qBrowser.Value
                If qBrowser.Seek("props\pageProps\mainColumnData\prestigiousAwardSummary\award\text") Then Me.awards.awardText = qBrowser.Value
                If qBrowser.Seek("props\pageProps\mainColumnData\prestigiousAwardSummary\award\id") Then Me.awards.awardId = qBrowser.Value
                If qBrowser.Seek("props\pageProps\mainColumnData\prestigiousAwardSummary\award\event\id") Then Me.awards.eventId = qBrowser.Value
                If qBrowser.Seek("props\pageProps\mainColumnData\ratingsSummary\topRanking\rank") Then Me.awards.rank = qBrowser.Value
                If qBrowser.Seek("props\pageProps\mainColumnData\directors") Then Me.directors = Person.parseJsonToList(qBrowser.Truncate)
                If qBrowser.Seek("props\pageProps\mainColumnData\writers") Then Me.writers = Person.parseJsonToList(qBrowser.Truncate)
                If qBrowser.Seek("props\pageProps\mainColumnData\cast") Then Me.actors = Person.parseJsonToList(qBrowser.Truncate)
                If qBrowser.Seek("props\pageProps\aboveTheFoldData\genres") Then Me.genres = Genre.parseJsonToList(qBrowser.Truncate)
                If qBrowser.Seek("props\pageProps\aboveTheFoldData\production") Then Me.companies = Company.parseJsonToList(qBrowser.Truncate)
                If qBrowser.Seek("props\pageProps\mainColumnData\countriesOfOrigin") Then Me.countries = Country.parseJsonToList(qBrowser.Truncate)
                If qBrowser.Seek("props\pageProps\mainColumnData\spokenLanguages") Then Me.languages = Language.parseJsonToList(qBrowser.Truncate)
                If qBrowser.Seek("props\pageProps\aboveTheFoldData\certificate\rating") Then Me.contentRating = qBrowser.Value
                If qBrowser.Seek("props\pageProps\aboveTheFoldData\ratingsSummary\aggregateRating") Then Me.rating.aggregateRating = qBrowser.Value
                If qBrowser.Seek("props\pageProps\aboveTheFoldData\ratingsSummary\voteCount") Then Me.rating.voteCount = qBrowser.Value
                If qBrowser.Seek("props\pageProps\aboveTheFoldData\meterRanking\currentRank") Then Me.rating.currentRank = qBrowser.Value
                If qBrowser.Seek("props\pageProps\aboveTheFoldData\meterRanking\rankChange\changeDirection") Then Me.rating.changeDirection = qBrowser.Value
                If qBrowser.Seek("props\pageProps\aboveTheFoldData\meterRanking\rankChange\difference") Then Me.rating.difference = qBrowser.Value
                If qBrowser.Seek("props\pageProps\aboveTheFoldData\metacritic\metascore\score") Then Me.metascore = qBrowser.Value
                If qBrowser.Seek("props\pageProps\mainColumnData\productionBudget\budget\amount") Then Me.boxOffice.productionBudget.amount = qBrowser.Value
                If qBrowser.Seek("props\pageProps\mainColumnData\productionBudget\budget\currency") Then Me.boxOffice.productionBudget.currency = qBrowser.Value
                If qBrowser.Seek("props\pageProps\mainColumnData\lifetimeGross\total\amount") Then Me.boxOffice.lifetimeGross.amount = qBrowser.Value
                If qBrowser.Seek("props\pageProps\mainColumnData\lifetimeGross\total\currency") Then Me.boxOffice.lifetimeGross.currency = qBrowser.Value
                If qBrowser.Seek("props\pageProps\mainColumnData\openingWeekendGross\gross\total\amount") Then Me.boxOffice.openingWeekendGross.amount = qBrowser.Value
                If qBrowser.Seek("props\pageProps\mainColumnData\openingWeekendGross\gross\total\currency") Then Me.boxOffice.openingWeekendGross.currency = qBrowser.Value
                If qBrowser.Seek("props\pageProps\mainColumnData\worldwideGross\total\amount") Then Me.boxOffice.worldwideGross.amount = qBrowser.Value
                If qBrowser.Seek("props\pageProps\mainColumnData\worldwideGross\total\currency") Then Me.boxOffice.worldwideGross.currency = qBrowser.Value
                If qBrowser.Seek("props\pageProps\mainColumnData\titleMainImages") Then Me.photos = Photo.parseJsonToList(qBrowser.Truncate)
                If qBrowser.Seek("props\pageProps\aboveTheFoldData\primaryVideos") Then Me.videos = Video.parseJsonToList(qBrowser.Truncate)
                If qBrowser.Seek("props\pageProps\aboveTheFoldData\keywords") Then Me.keywords = Extractor.getListOfString(qBrowser.Truncate)
                If qBrowser.Seek("props\pageProps\mainColumnData\moreLikeThisTitles") Then Me.similars = parseTitleList(qBrowser.Truncate)
                If qBrowser.Seek("props\pageProps\mainColumnData\episodes") Then Me.episodes.parseJson(qBrowser.Truncate)

                parseJson = True

            End If

        Catch nException As Exception

            nException = Nothing

        Finally

            qBrowser = Nothing

        End Try

    End Function

    Public Function json() As String

        json = ""

        Try

            json = Newtonsoft.Json.JsonConvert.SerializeObject(Me)

        Catch nException As Exception

            nException = Nothing

        End Try

    End Function

#End Region

#Region " Property "

    Public Property id As String = Nothing
    Public Property title As String = Nothing
    Public Property originalTitle As String = Nothing
    Public Property type As String = Nothing
    Public Property year As String = Nothing
    Public Property image As Photo = Nothing
    Public Property releaseDate As String = Nothing
    Public Property runtime As Runtime = Nothing
    Public Property plot As String = Nothing
    Public Property awards As Awards = Nothing
    Public Property directors As List(Of Person) = Nothing
    Public Property writers As List(Of Person) = Nothing
    Public Property actors As List(Of Person) = Nothing
    Public Property genres As List(Of Genre) = Nothing
    Public Property companies As List(Of Company) = Nothing
    Public Property countries As List(Of Country) = Nothing
    Public Property languages As List(Of Language) = Nothing
    Public Property contentRating As String = Nothing
    Public Property rating As Rating = Nothing
    Public Property metascore As String = Nothing
    Public Property videos As List(Of Video) = Nothing
    Public Property photos As List(Of Photo) = Nothing
    Public Property boxOffice As BoxOffice = Nothing
    Public Property keywords As List(Of String) = Nothing
    Public Property similars As List(Of Title) = Nothing
    Public Property episodes As Episodes = Nothing
    Public Property errorMessage As String = Nothing

#End Region

End Class

Public Class Name

#Region " Builders "

    Public Sub New()

        Try

            Me.Initialize()

        Catch nException As Exception

            nException = Nothing

        End Try

    End Sub

#End Region

#Region " Private "

    Private Sub Initialize()

        Try

            Me.id = ""
            Me.name = ""
            Me.knownFor = New List(Of KnownFor)
            Me.primaryImage = New Photo
            Me.bio = ""
            Me.primaryProfessions = New List(Of String)
            Me.birthDate = New detailsDate
            Me.deathDate = New detailsDate
            Me.meterRanking = New MeterRanking
            Me.primaryVideos = New List(Of Video)
            Me.award = New Awards
            Me.images = New List(Of Photo)
            Me.releasedPrimaryCredits = New List(Of ReleasedCredit)
            Me.unreleasedPrimaryCredits = New List(Of ReleasedCredit)
            Me.jobs = New List(Of Job)
            Me.akas = New List(Of String)
            Me.otherWorks = New List(Of String)
            Me.parents = New List(Of Parent)
            Me.children = New List(Of Parent)
            Me.others = New List(Of Parent)
            Me.personalExternalLinks = New List(Of ExternalLink)
            Me.quotes = New List(Of String)
            Me.nickNames = New List(Of String)
            Me.errorMessage = ""

        Catch nException As Exception

            nException = Nothing

        End Try

    End Sub

#End Region

#Region " Methods "

    Public Overrides Function ToString() As String

        ToString = "<name>"

        Try

            If Me.name = "" Then ToString = "<empty>" Else ToString = Me.name

        Catch nException As Exception

            nException = Nothing

        End Try

    End Function

    Public Function parseJson(qJson As String) As Boolean

        parseJson = False

        Dim nBrowser As Browser = Nothing

        Try

            nBrowser = New Browser(qJson)
            parseJson = parseJson(nBrowser)

        Catch nException As Exception

            nException = Nothing

        Finally

            nBrowser = Nothing

            qJson = Nothing

        End Try

    End Function

    Public Function parseJson(qBrowser As Browser) As Boolean

        parseJson = False

        Dim nJob As Job = Nothing
        Dim nVideo As Video = Nothing
        Dim nPhoto As Photo = Nothing
        Dim nParent As Parent = Nothing
        Dim nBrowser As Browser = Nothing
        Dim nKnownFor As KnownFor = Nothing
        Dim nExternalLink As ExternalLink = Nothing
        Dim nReleasedCredit As ReleasedCredit = Nothing

        Try

            If qBrowser.Seek("props\pageProps\aboveTheFold\id") Then Me.id = qBrowser.Value
            If qBrowser.Seek("props\pageProps\aboveTheFold\nameText\text") Then Me.name = qBrowser.Value
            If qBrowser.Seek("props\pageProps\aboveTheFold\knownFor\edges") Then
                For Each nBrowser In qBrowser.List
                    nKnownFor = New KnownFor
                    If nKnownFor.parseJson(nBrowser) Then Me.knownFor.Add(nKnownFor)
                Next
            End If
            If qBrowser.Seek("props\pageProps\mainColumnData\primaryImage") Then Me.primaryImage.parseJson(qBrowser.Truncate)
            If qBrowser.Seek("props\pageProps\aboveTheFold\bio\text\plainText") Then Me.bio = qBrowser.Value
            If qBrowser.Seek("props\pageProps\mainColumnData\primaryProfessions") Then
                For Each nBrowser In qBrowser.List
                    If nBrowser.Seek("category\text") Then Me.primaryProfessions.Add(nBrowser.Value)
                Next
            End If
            If qBrowser.Seek("props\pageProps\aboveTheFold\birthDate") Then Me.birthDate.parseJson(qBrowser.Truncate)
            If qBrowser.Seek("props\pageProps\aboveTheFold\deathDate") Then Me.deathDate.parseJson(qBrowser.Truncate)
            If qBrowser.Seek("props\pageProps\mainColumnData\birthLocation") Then Me.birthDate.parseJson(qBrowser.Truncate)
            If qBrowser.Seek("props\pageProps\mainColumnData\deathLocation") Then Me.deathDate.parseJson(qBrowser.Truncate)
            If qBrowser.Seek("props\pageProps\aboveTheFold\meterRanking") Then Me.meterRanking.parseJson(qBrowser.Truncate)
            If qBrowser.Seek("props\pageProps\aboveTheFold\primaryVideos\edges") Then
                For Each nBrowser In qBrowser.List
                    nVideo = New Video
                    If nVideo.parseJson(nBrowser.Truncate) Then Me.primaryVideos.Add(nVideo)
                Next
            End If
            If qBrowser.Seek("props\pageProps\mainColumnData") Then Me.award.parseJson(qBrowser.Truncate)
            If qBrowser.Seek("props\pageProps\mainColumnData\images\edges") Then
                For Each nBrowser In qBrowser.List
                    nPhoto = New Photo
                    If nPhoto.parseJson(nBrowser.Truncate) Then Me.images.Add(nPhoto)
                Next
            End If
            If qBrowser.Seek("props\pageProps\mainColumnData\releasedPrimaryCredits") Then
                For Each nBrowser In qBrowser.List
                    nReleasedCredit = New ReleasedCredit
                    If nReleasedCredit.parseJson(nBrowser.Truncate) Then Me.releasedPrimaryCredits.Add(nReleasedCredit)
                Next
            End If
            If qBrowser.Seek("props\pageProps\mainColumnData\unreleasedPrimaryCredits") Then
                For Each nBrowser In qBrowser.List
                    nReleasedCredit = New ReleasedCredit
                    If nReleasedCredit.parseJson(nBrowser.Truncate) Then Me.unreleasedPrimaryCredits.Add(nReleasedCredit)
                Next
            End If
            If qBrowser.Seek("props\pageProps\mainColumnData\jobs") Then
                For Each nBrowser In qBrowser.List
                    nJob = New Job
                    If nJob.parseJson(nBrowser.Truncate) Then Me.jobs.Add(nJob)
                Next
            End If
            If qBrowser.Seek("props\pageProps\mainColumnData\akas\edges") Then
                For Each nBrowser In qBrowser.List
                    If nBrowser.Seek("node\displayableProperty\value\plainText") Then Me.akas.Add(nBrowser.Value)
                Next
            End If
            If qBrowser.Seek("props\pageProps\mainColumnData\otherWorks\edges") Then
                For Each nBrowser In qBrowser.List
                    If nBrowser.Seek("node\text\plaidHtml") Then Me.otherWorks.Add(nBrowser.Value)
                Next
            End If
            If qBrowser.Seek("props\pageProps\mainColumnData\parents\edges") Then
                For Each nBrowser In qBrowser.List
                    nParent = New Parent
                    If nParent.parseJson(nBrowser) Then Me.parents.Add(nParent)
                Next
            End If
            If qBrowser.Seek("props\pageProps\mainColumnData\children\edges") Then
                For Each nBrowser In qBrowser.List
                    nParent = New Parent
                    If nParent.parseJson(nBrowser) Then Me.children.Add(nParent)
                Next
            End If
            If qBrowser.Seek("props\pageProps\mainColumnData\others\edges") Then
                For Each nBrowser In qBrowser.List
                    nParent = New Parent
                    If nParent.parseJson(nBrowser) Then Me.others.Add(nParent)
                Next
            End If
            If qBrowser.Seek("props\pageProps\mainColumnData\personalDetailsExternalLinks\edges") Then
                For Each nBrowser In qBrowser.List
                    nExternalLink = New ExternalLink
                    If nExternalLink.parseJson(nBrowser) Then Me.personalExternalLinks.Add(nExternalLink)
                Next
            End If
            If qBrowser.Seek("props\pageProps\mainColumnData\quotes\edges") Then
                For Each nBrowser In qBrowser.List
                    If nBrowser.Seek("node\displayableArticle\body\plaidHtml") Then Me.quotes.Add(nBrowser.Value)
                Next
            End If
            If qBrowser.Seek("props\pageProps\mainColumnData\nickNames") Then
                For Each nBrowser In qBrowser.List
                    If nBrowser.Seek("displayableProperty\value\plainText") Then Me.nickNames.Add(nBrowser.Value)
                Next
            End If

            parseJson = True

        Catch nException As Exception

            nException = Nothing

        Finally

            nJob = Nothing
            nVideo = Nothing
            nPhoto = Nothing
            nParent = Nothing
            nBrowser = Nothing
            nKnownFor = Nothing
            nExternalLink = Nothing
            nReleasedCredit = Nothing

            qBrowser = Nothing

        End Try

    End Function

    Public Function json() As String

        json = ""

        Try

            json = Newtonsoft.Json.JsonConvert.SerializeObject(Me)

        Catch nException As Exception

            nException = Nothing

        End Try

    End Function

#End Region

#Region " Property "

    Public Property id As String = Nothing
    Public Property name As String = Nothing
    Public Property knownFor As List(Of KnownFor) = Nothing
    Public Property primaryImage As Photo = Nothing
    Public Property bio As String = Nothing
    Public Property primaryProfessions As List(Of String) = Nothing
    Public Property birthDate As detailsDate = Nothing
    Public Property deathDate As detailsDate = Nothing
    Public Property meterRanking As MeterRanking = Nothing
    Public Property primaryVideos As List(Of Video) = Nothing
    Public Property award As Awards = Nothing
    Public Property images As List(Of Photo) = Nothing
    Public Property releasedPrimaryCredits As List(Of ReleasedCredit) = Nothing
    Public Property unreleasedPrimaryCredits As List(Of ReleasedCredit) = Nothing
    Public Property jobs As List(Of Job) = Nothing
    Public Property akas As List(Of String) = Nothing
    Public Property otherWorks As List(Of String) = Nothing
    Public Property parents As List(Of Parent) = Nothing
    Public Property children As List(Of Parent) = Nothing
    Public Property others As List(Of Parent) = Nothing
    Public Property personalExternalLinks As List(Of ExternalLink) = Nothing
    Public Property quotes As List(Of String) = Nothing
    Public Property nickNames As List(Of String) = Nothing
    Public Property errorMessage As String = Nothing

#End Region

End Class

Public Class ReleasedCredit

#Region " Builders "

    Public Sub New()

        Try

            Me.Initialize()

        Catch nException As Exception

            nException = Nothing

        End Try

    End Sub

#End Region

#Region " Private "

    Private Sub Initialize()

        Try

            Me.category = ""
            Me.credits = New List(Of Credit)

        Catch nException As Exception

            nException = Nothing

        End Try

    End Sub

#End Region

#Region " Methods "

    Public Overrides Function ToString() As String

        ToString = "<ReleasedCredit>"

        Try

            If Me.category = "" Then ToString = "<empty>" Else ToString = Me.category

        Catch nException As Exception

            nException = Nothing

        End Try

    End Function

    Public Function parseJson(qJson As String) As Boolean

        parseJson = False

        Dim nBrowser As Browser = Nothing

        Try

            nBrowser = New Browser(qJson)
            parseJson = parseJson(nBrowser)

        Catch nException As Exception

            nException = Nothing

        Finally

            nBrowser = Nothing

            qJson = Nothing

        End Try

    End Function

    Public Function parseJson(qBrowser As Browser) As Boolean

        parseJson = False

        Dim nCredit As Credit = Nothing
        Dim nBrowser As Browser = Nothing

        Try

            If qBrowser.Seek("category\text") Then Me.category = qBrowser.Value
            If qBrowser.Seek("credits\edges") Then
                For Each nBrowser In qBrowser.List
                    nCredit = New Credit
                    If nCredit.parseJson(nBrowser) Then Me.credits.Add(nCredit)
                Next
            End If

            parseJson = True

        Catch nException As Exception

            nException = Nothing

        Finally

            nCredit = Nothing
            nBrowser = Nothing

            qBrowser = Nothing

        End Try

    End Function

#End Region

#Region " Property "

    Public Property category As String = Nothing
    Public Property credits As List(Of Credit) = Nothing

#End Region

End Class

Public Class Credit

#Region " Builders "

    Public Sub New()

        Try

            Me.Initialize()

        Catch nException As Exception

            nException = Nothing

        End Try

    End Sub

#End Region

#Region " Private "

    Private Sub Initialize()

        Try

            Me.title = New Title
            Me.jobs = New List(Of String)
            Me.attributes = New List(Of String)
            Me.characters = New List(Of String)
            Me.category = ""

        Catch nException As Exception

            nException = Nothing

        End Try

    End Sub

#End Region

#Region " Methods "

    Public Overrides Function ToString() As String

        ToString = "<Credit>"

        Try

            If Not Me.category = "" Then ToString = Me.category

        Catch nException As Exception

            nException = Nothing

        End Try

    End Function

    Public Function parseJson(qJson As String) As Boolean

        parseJson = False

        Dim nBrowser As Browser = Nothing

        Try

            nBrowser = New Browser(qJson)
            parseJson = parseJson(nBrowser)

        Catch nException As Exception

            nException = Nothing

        Finally

            nBrowser = Nothing

            qJson = Nothing

        End Try

    End Function

    Public Function parseJson(qBrowser As Browser) As Boolean

        parseJson = False

        Dim nTitle As Title = Nothing
        Dim nBrowser As Browser = Nothing

        Try

            If qBrowser.Seek("node\category\text") Then Me.category = qBrowser.Value
            If qBrowser.Seek("node\jobs") Then
                For Each nBrowser In qBrowser.List
                    If nBrowser.Seek("text") Then Me.jobs.Add(nBrowser.Value)
                Next
            End If
            If qBrowser.Seek("node\attributes") Then
                For Each nBrowser In qBrowser.List
                    If nBrowser.Seek("text") Then Me.attributes.Add(nBrowser.Value)
                Next
            End If
            If qBrowser.Seek("node\title") Then Me.title.parseJson(qBrowser.Truncate)

            parseJson = True

        Catch nException As Exception

            nException = Nothing

        Finally

            nTitle = Nothing
            nBrowser = Nothing

            qBrowser = Nothing

        End Try

    End Function

#End Region

#Region " Property "

    Public Property title As Title = Nothing
    Public Property category As String = Nothing
    Public Property characters As List(Of String) = Nothing
    Public Property jobs As List(Of String) = Nothing
    Public Property attributes As List(Of String) = Nothing

#End Region

End Class

Public Class KnownFor

#Region " Shared "

    Public Shared Function parseJson(qBrowser As Browser, ByRef qKnownFor As KnownFor) As Boolean

        parseJson = False
        qKnownFor = New KnownFor

        Try

            parseJson = qKnownFor.parseJson(qBrowser)

        Catch nException As Exception

            nException = Nothing

        Finally

            qBrowser = Nothing

        End Try

    End Function

#End Region

#Region " Builders "

    Public Sub New()

        Try

            Me.Initialize()

        Catch nException As Exception

            nException = Nothing

        End Try

    End Sub

#End Region

#Region " Private "

    Private Sub Initialize()

        Try

            Me.titleText = ""
            Me.pricipalCategory = ""

        Catch nException As Exception

            nException = Nothing

        End Try

    End Sub

#End Region

#Region " Methods "

    Public Overrides Function ToString() As String

        ToString = "<KnownFor>"

        Dim nResult As String = Nothing

        Try

            nResult = ""
            If Not Me.pricipalCategory = "" Then nResult = "[" & Me.pricipalCategory & "]"
            If Not Me.titleText = "" Then
                If nResult.Length > 0 Then nResult = nResult & " - "
                nResult = nResult & Me.titleText
            End If

            ToString = nResult

        Catch nException As Exception

            nException = Nothing

        Finally

            nResult = Nothing

        End Try

    End Function

    Public Function parseJson(qJson As String) As Boolean

        parseJson = False

        Dim nBrowser As Browser = Nothing

        Try

            nBrowser = New Browser(qJson)
            parseJson = parseJson(nBrowser)

        Catch nException As Exception

            nException = Nothing

        Finally

            nBrowser = Nothing

            qJson = Nothing

        End Try

    End Function

    Public Function parseJson(qBrowser As Browser) As Boolean

        parseJson = False

        Try

            If qBrowser.Seek("node\title\titleText\text") Then Me.titleText = qBrowser.Value : parseJson = True
            If qBrowser.Seek("node\summary\principalCategory\text") Then Me.pricipalCategory = qBrowser.Value : parseJson = True

        Catch nException As Exception

            nException = Nothing

        Finally

            qBrowser = Nothing

        End Try

    End Function

#End Region

#Region " Property "

    Public Property titleText As String = Nothing
    Public Property pricipalCategory As String = Nothing

#End Region

End Class

Public Class Job

#Region " Shared "

    Public Shared Function parseJson(qBrowser As Browser, ByRef qJob As Job) As Boolean

        parseJson = False
        qJob = New Job

        Try

            parseJson = qJob.parseJson(qBrowser)

        Catch nException As Exception

            nException = Nothing

        Finally

            qBrowser = Nothing

        End Try

    End Function

#End Region

#Region " Builders "

    Public Sub New()

        Try

            Me.Initialize()

        Catch nException As Exception

            nException = Nothing

        End Try

    End Sub

#End Region

#Region " Private "

    Private Sub Initialize()

        Try

            Me.category = ""
            Me.credits = ""

        Catch nException As Exception

            nException = Nothing

        End Try

    End Sub

#End Region

#Region " Methods "

    Public Overrides Function ToString() As String

        ToString = "<Job>"

        Dim nResult As String = Nothing

        Try

            nResult = ""
            If Not Me.category = "" Then nResult = "Category: " & Me.category
            If Not Me.credits = "" Then
                If nResult.Length > 0 Then nResult = nResult & " "
                nResult = nResult & "Credits: " & Me.credits
            End If

            ToString = nResult

        Catch nException As Exception

            nException = Nothing

        Finally

            nResult = Nothing

        End Try

    End Function

    Public Function parseJson(qJson As String) As Boolean

        parseJson = False

        Dim nBrowser As Browser = Nothing

        Try

            nBrowser = New Browser(qJson)
            parseJson = parseJson(nBrowser)

        Catch nException As Exception

            nException = Nothing

        Finally

            nBrowser = Nothing

            qJson = Nothing

        End Try

    End Function

    Public Function parseJson(qBrowser As Browser) As Boolean

        parseJson = False

        Try

            If qBrowser.Seek("category\text") Then Me.category = qBrowser.Value : parseJson = True
            If qBrowser.Seek("credits\total") Then Me.credits = qBrowser.Value : parseJson = True

        Catch nException As Exception

            nException = Nothing

        Finally

            qBrowser = Nothing

        End Try

    End Function

#End Region

#Region " Property "

    Public Property category As String = Nothing
    Public Property credits As String = Nothing

#End Region

End Class

Public Class ExternalLink

#Region " Shared "

    Public Shared Function parseJson(qBrowser As Browser, ByRef qExternalLink As ExternalLink) As Boolean

        parseJson = False
        qExternalLink = New ExternalLink

        Try

            parseJson = qExternalLink.parseJson(qBrowser)

        Catch nException As Exception

            nException = Nothing

        Finally

            qBrowser = Nothing

        End Try

    End Function

#End Region

#Region " Builders "

    Public Sub New()

        Try

            Me.Initialize()

        Catch nException As Exception

            nException = Nothing

        End Try

    End Sub

#End Region

#Region " Private "

    Private Sub Initialize()

        Try

            Me.url = ""
            Me.label = ""

        Catch nException As Exception

            nException = Nothing

        End Try

    End Sub

#End Region

#Region " Methods "

    Public Overrides Function ToString() As String

        ToString = "<ExternalLink>"

        Dim nResult As String = Nothing

        Try

            nResult = ""
            If Not Me.label = "" Then nResult = Me.label
            If Not Me.url = "" AndAlso nResult.Length = 0 Then nResult = Me.url

            ToString = nResult

        Catch nException As Exception

            nException = Nothing

        Finally

            nResult = Nothing

        End Try

    End Function

    Public Function parseJson(qJson As String) As Boolean

        parseJson = False

        Dim nBrowser As Browser = Nothing

        Try

            nBrowser = New Browser(qJson)
            parseJson = parseJson(nBrowser)

        Catch nException As Exception

            nException = Nothing

        Finally

            nBrowser = Nothing

            qJson = Nothing

        End Try

    End Function

    Public Function parseJson(qBrowser As Browser) As Boolean

        parseJson = False

        Try

            If qBrowser.Seek("node\url") Then Me.url = qBrowser.Value : parseJson = True
            If qBrowser.Seek("node\label") Then Me.label = qBrowser.Value : parseJson = True

        Catch nException As Exception

            nException = Nothing

        Finally

            qBrowser = Nothing

        End Try

    End Function

#End Region

#Region " Property "

    Public Property url As String = Nothing
    Public Property label As String = Nothing

#End Region

End Class

Public Class Parent

#Region " Shared "

    Public Shared Function parseJson(qBrowser As Browser, ByRef qParent As Parent) As Boolean

        parseJson = False
        qParent = New Parent

        Try

            parseJson = qParent.parseJson(qBrowser)

        Catch nException As Exception

            nException = Nothing

        Finally

            qBrowser = Nothing

        End Try

    End Function

#End Region

#Region " Builders "

    Public Sub New()

        Try

            Me.Initialize()

        Catch nException As Exception

            nException = Nothing

        End Try

    End Sub

#End Region

#Region " Private "

    Private Sub Initialize()

        Try

            Me.id = ""
            Me.name = ""
            Me.relationship = ""

        Catch nException As Exception

            nException = Nothing

        End Try

    End Sub

#End Region

#Region " Methods "

    Public Overrides Function ToString() As String

        ToString = "<Parent>"

        Dim nResult As String = Nothing

        Try

            nResult = ""
            If Not Me.name = "" Then nResult = Me.name
            If Not Me.relationship = "" Then
                If nResult.Length > 0 Then nResult = nResult & " "
                nResult = nResult & "(" & Me.relationship & ")"
            End If

            ToString = nResult

        Catch nException As Exception

            nException = Nothing

        Finally

            nResult = Nothing

        End Try

    End Function

    Public Function parseJson(qJson As String) As Boolean

        parseJson = False

        Dim nBrowser As Browser = Nothing

        Try

            nBrowser = New Browser(qJson)
            parseJson = parseJson(nBrowser)

        Catch nException As Exception

            nException = Nothing

        Finally

            nBrowser = Nothing

            qJson = Nothing

        End Try

    End Function

    Public Function parseJson(qBrowser As Browser) As Boolean

        parseJson = False

        Try

            If qBrowser.Seek("node\relationName\name\id") Then Me.id = qBrowser.Value : parseJson = True
            If qBrowser.Seek("node\relationshipType\text") Then Me.relationship = qBrowser.Value : parseJson = True
            If qBrowser.Seek("node\relationName\displayableProperty\value\plainText") Then Me.name = qBrowser.Value : parseJson = True

        Catch nException As Exception

            nException = Nothing

        Finally

            qBrowser = Nothing

        End Try

    End Function

#End Region

#Region " Property "

    Public Property id As String = Nothing
    Public Property name As String = Nothing
    Public Property relationship As String = Nothing

#End Region

End Class

Public Class detailsDate

#Region " Builders "

    Public Sub New()

        Try

            Me.Initialize()

        Catch nException As Exception

            nException = Nothing

        End Try

    End Sub

#End Region

#Region " Private "

    Private Sub Initialize()

        Try

            Me.day = ""
            Me.year = ""
            Me.month = ""
            Me.location = ""
            Me.plainText = ""

        Catch nException As Exception

            nException = Nothing

        End Try

    End Sub

#End Region

#Region " Methods "

    Public Overrides Function ToString() As String

        ToString = "<detailsDate>"

        Dim nResult As String = Nothing

        Try

            If Me.plainText = "" Then nResult = "<empty>" Else nResult = Me.plainText
            ToString = nResult

        Catch nException As Exception

            nException = Nothing

        Finally

            nResult = Nothing

        End Try

    End Function

    Public Function parseJson(qJson As String) As Boolean

        parseJson = False

        Dim nBrowser As Browser = Nothing

        Try

            nBrowser = New Browser(qJson)
            parseJson = parseJson(nBrowser)

        Catch nException As Exception

            nException = Nothing

        Finally

            nBrowser = Nothing

            qJson = Nothing

        End Try

    End Function

    Public Function parseJson(qBrowser As Browser) As Boolean

        parseJson = False

        Try

            If qBrowser.Seek("dateComponents\day") Then Me.day = qBrowser.Value
            If qBrowser.Seek("dateComponents\year") Then Me.year = qBrowser.Value
            If qBrowser.Seek("dateComponents\month") Then Me.month = qBrowser.Value
            If qBrowser.Seek("displayableProperty\value\plainText") Then Me.plainText = qBrowser.Value
            If qBrowser.Seek("birthLocation\text") Then Me.location = qBrowser.Value

        Catch nException As Exception

            nException = Nothing

        Finally

            qBrowser = Nothing

        End Try

    End Function

#End Region

#Region " Property "

    Public Property day As String = Nothing
    Public Property month As String = Nothing
    Public Property year As String = Nothing
    Public Property location As String = Nothing
    Public Property plainText As String = Nothing

#End Region

End Class

Public Class MeterRanking

#Region " Builders "

    Public Sub New()

        Try

            Me.Initialize()

        Catch nException As Exception

            nException = Nothing

        End Try

    End Sub

#End Region

#Region " Private "

    Private Sub Initialize()

        Try

            Me.difference = ""
            Me.currentRank = ""
            Me.changeDirection = ""

        Catch nException As Exception

            nException = Nothing

        End Try

    End Sub

#End Region

#Region " Methods "

    Public Overrides Function ToString() As String

        ToString = "<MeterRanking>"

        Dim nResult As String = Nothing

        Try

            If Me.currentRank = "" Then nResult = "<empty>" Else nResult = "Rank: " & Me.currentRank
            ToString = nResult

        Catch nException As Exception

            nException = Nothing

        Finally

            nResult = Nothing

        End Try

    End Function

    Public Function parseJson(qJson As String) As Boolean

        parseJson = False

        Dim nBrowser As Browser = Nothing

        Try

            nBrowser = New Browser(qJson)
            parseJson = parseJson(nBrowser)

        Catch nException As Exception

            nException = Nothing

        Finally

            nBrowser = Nothing

            qJson = Nothing

        End Try

    End Function

    Public Function parseJson(qBrowser As Browser) As Boolean

        parseJson = False

        Try

            If qBrowser.Seek("currentRank") Then Me.currentRank = qBrowser.Value : parseJson = True
            If qBrowser.Seek("rankChange\difference") Then Me.difference = qBrowser.Value : parseJson = True
            If qBrowser.Seek("rankChange\changeDirection") Then Me.changeDirection = qBrowser.Value : parseJson = True

        Catch nException As Exception

            nException = Nothing

        Finally

            qBrowser = Nothing

        End Try

    End Function

#End Region

#Region " Property "

    Public Property currentRank As String = Nothing
    Public Property changeDirection As String = Nothing
    Public Property difference As String = Nothing

#End Region

End Class

Public Class Runtime

#Region " Builders "

    Public Sub New()

        Try

            Me.Initialize()

        Catch nException As Exception

            nException = Nothing

        End Try

    End Sub

#End Region

#Region " Private "

    Private Sub Initialize()

        Try

            Me.seconds = ""
            Me.plainText = ""

        Catch nException As Exception

            nException = Nothing

        End Try

    End Sub

#End Region

#Region " Methods "

    Public Overrides Function ToString() As String

        ToString = "<Runtime>"

        Try

            If Not Me.plainText = "" Then
                ToString = Me.plainText
            ElseIf Not Me.seconds = "" Then
                ToString = Me.seconds
            Else
                ToString = "<empty>"
            End If

        Catch nException As Exception

            nException = Nothing

        End Try

    End Function

    Public Function fromSeconds(qSeconds As String) As Boolean

        fromSeconds = False

        Dim nHours As Integer = Nothing
        Dim nRemain As Integer = Nothing
        Dim nMinutes As Integer = Nothing
        Dim nSeconds As Integer = Nothing

        Try

            If Integer.TryParse(qSeconds, nSeconds) Then

                nHours = nSeconds \ 3600
                nMinutes = (nSeconds Mod 3600) \ 60
                nRemain = nSeconds Mod 60

                Me.seconds = nSeconds

                If nHours = 0 Then
                    Me.plainText = String.Format("{0}m {1}s", nMinutes, nRemain)
                Else
                    Me.plainText = String.Format("{0}h {1}m", nHours, nMinutes)
                End If

            End If

        Catch nException As Exception

            nException = Nothing

        Finally

            nHours = Nothing
            nRemain = Nothing
            nMinutes = Nothing
            nSeconds = Nothing

            qSeconds = Nothing

        End Try

    End Function

#End Region

#Region " Property "

    Public Property seconds As String = Nothing
    Public Property plainText As String = Nothing

#End Region

End Class

Public Class Awards

#Region " Builders "

    Public Sub New()

        Try

            Me.Initialize()

        Catch nException As Exception

            nException = Nothing

        End Try

    End Sub

#End Region

#Region " Private "

    Private Sub Initialize()

        Try

            Me.rank = ""
            Me.wins = ""
            Me.eventId = ""
            Me.awardId = ""
            Me.awardText = ""
            Me.awardWins = ""
            Me.nomination = ""
            Me.awardNominations = ""

        Catch nException As Exception

            nException = Nothing

        End Try

    End Sub

#End Region

#Region " Methods "

    Public Overrides Function ToString() As String

        ToString = "<Awards>"

        Dim nResult As String = Nothing

        Try

            nResult = ""

            If Not Me.rank = "" Then
                nResult = "Top rated movie #" & Me.rank
            End If

            If Not Me.awardText = "" AndAlso Not Me.awardWins = "" Then
                If nResult.Length > 0 Then nResult = nResult & " | "
                nResult = nResult & "Won " & Me.awardWins & " " & Me.awardText
            End If

            If Not Me.wins = "" AndAlso Not Me.nomination = "" Then
                If nResult.Length > 0 Then nResult = nResult & " | "
                nResult = nResult & Me.wins & " wins And " & Me.nomination & " nominations total."
            End If

            If nResult = "" Then ToString = "<empty>" Else ToString = nResult

        Catch nException As Exception

            nException = Nothing

        Finally

            nResult = Nothing

        End Try

    End Function

    Public Function parseJson(qBrowser As Browser) As Boolean

        parseJson = False

        Try

            If qBrowser.Seek("wins\total") Then Me.wins = qBrowser.Value
            If qBrowser.Seek("nominations\total") Then Me.nomination = qBrowser.Value
            If qBrowser.Seek("prestigiousAwardSummary\nominations") Then Me.awardNominations = qBrowser.Value
            If qBrowser.Seek("prestigiousAwardSummary\wins") Then Me.awardWins = qBrowser.Value
            If qBrowser.Seek("prestigiousAwardSummary\award\text") Then Me.awardText = qBrowser.Value
            If qBrowser.Seek("prestigiousAwardSummary\award\id") Then Me.awardId = qBrowser.Value
            If qBrowser.Seek("prestigiousAwardSummary\award\event\id") Then Me.eventId = qBrowser.Value

            parseJson = True

        Catch nException As Exception

            nException = Nothing

        Finally

            qBrowser = Nothing

        End Try

    End Function

#End Region

#Region " Property "

    Public Property rank As String = Nothing
    Public Property wins As String = Nothing
    Public Property eventId As String = Nothing
    Public Property awardId As String = Nothing
    Public Property awardText As String = Nothing
    Public Property awardWins As String = Nothing
    Public Property awardNominations As String = Nothing
    Public Property nomination As String = Nothing

#End Region

End Class

Public Class Person

#Region " Shared "

    Public Shared Function parseJson(qJObject As JObject, ByRef qPerson As Person) As Boolean

        parseJson = False
        qPerson = New Person

        Try

            parseJson = qPerson.parseJson(qJObject)

        Catch nException As Exception

            nException = Nothing

        Finally

            qJObject = Nothing

        End Try

    End Function

    Public Shared Function parseCreditsForCategory(qJArray As JArray) As List(Of Person)

        parseCreditsForCategory = New List(Of Person)

        Dim nItem As JObject = Nothing
        Dim nPerson As Person = Nothing
        Dim nCredit As JObject = Nothing
        Dim nCategory As String = Nothing
        Dim nList As List(Of Person) = Nothing

        Try

            nList = New List(Of Person)
            If qJArray IsNot Nothing Then
                For Each nItem In qJArray
                    nCategory = ""
                    If nItem("category")("text") IsNot Nothing Then nCategory = nItem("category")("text")
                    If nItem("credits") IsNot Nothing Then
                        For Each nCredit In nItem("credits")
                            nPerson = New Person
                            nPerson.category = nCategory
                            If nCredit("name")("id") IsNot Nothing Then nPerson.id = nCredit("name")("id").ToString
                            If nCredit("name")("nameText")("text") IsNot Nothing Then nPerson.name = nCredit("name")("nameText")("text").ToString
                            nList.Add(nPerson)
                        Next
                    End If
                Next
            End If

            parseCreditsForCategory = nList

        Catch nException As Exception

            nException = Nothing

        Finally

            nItem = Nothing
            nList = Nothing
            nPerson = Nothing
            nCredit = Nothing
            nCategory = Nothing

            qJArray = Nothing

        End Try

    End Function

    Public Shared Function parseCreditEdge(qJObject As JObject) As List(Of Person)

        parseCreditEdge = New List(Of Person)

        Dim nEdge As JObject = Nothing
        Dim nPerson As Person = Nothing
        Dim nCharacter As JObject = Nothing
        Dim nList As List(Of Person) = Nothing

        Try

            nList = New List(Of Person)
            If qJObject IsNot Nothing AndAlso qJObject("edges") IsNot Nothing Then
                For Each nEdge In qJObject("edges")
                    nPerson = New Person
                    If nEdge("node")("name")("id") IsNot Nothing Then nPerson.id = nEdge("node")("name")("id").ToString
                    If nEdge("node")("name")("nameText")("text") IsNot Nothing Then nPerson.name = nEdge("node")("name")("nameText")("text").ToString
                    If nEdge("node")("name")("primaryImage") IsNot Nothing AndAlso nEdge("node")("name")("primaryImage").HasValues Then nPerson.image.parseJson(nEdge("node")("name")("primaryImage"))
                    If nEdge("node")("characters") IsNot Nothing Then
                        For Each nCharacter In nEdge("node")("characters")
                            If nCharacter("name") IsNot Nothing Then nPerson.characters.Add(nCharacter("name").ToString)
                        Next
                    End If
                    nList.Add(nPerson)
                Next
            End If

            parseCreditEdge = nList

        Catch nException As Exception

            nException = Nothing

        Finally

            nEdge = Nothing
            nList = Nothing
            nPerson = Nothing
            nCharacter = Nothing

            qJObject = Nothing

        End Try

    End Function

    Public Shared Function parseJsonToList(qBrowser As Browser) As List(Of Person)

        parseJsonToList = New List(Of Person)

        Dim nPerson As Person = Nothing
        Dim nCredit As Browser = Nothing
        Dim nBrowser As Browser = Nothing
        Dim nCharacter As Browser = Nothing
        Dim nList As List(Of Person) = Nothing

        Try

            nList = New List(Of Person)

            If qBrowser.Seek("__typename") Then
                Select Case qBrowser.Value
                    Case "CreditConnection"
                        qBrowser.Seek("edges")
                End Select
            End If

            For Each nBrowser In qBrowser.List
                If nBrowser.Seek("__typename") Then
                    Select Case nBrowser.Value
                        Case "PrincipalCreditsForCategory"
                            nPerson = New Person
                            If nBrowser.Seek("category\text") Then nPerson.category = nBrowser.Value
                            If nBrowser.Seek("credits") Then
                                For Each nCredit In nBrowser.List
                                    If nCredit.Seek("name\id") Then nPerson.id = nCredit.Value
                                    If nCredit.Seek("name\nameText\text") Then nPerson.name = nCredit.Value
                                Next
                            End If
                            nList.Add(nPerson)
                        Case "CreditEdge"
                            nPerson = New Person
                            If nBrowser.Seek("node\name\id") Then nPerson.id = nBrowser.Value
                            If nBrowser.Seek("node\name\nameText\text") Then nPerson.name = nBrowser.Value
                            If nBrowser.Seek("node\name\primaryImage") Then nPerson.image.parseJson(nBrowser.Truncate)
                            If nBrowser.Seek("node\category\id") Then nPerson.category = nBrowser.Value
                            If nBrowser.Seek("node\characters") Then
                                For Each nCharacter In nBrowser.List
                                    If nCharacter.Seek("name") Then nPerson.characters.Add(nCharacter.Value)
                                Next
                            End If
                            nList.Add(nPerson)
                    End Select
                End If
            Next

            parseJsonToList = nList

        Catch nException As Exception

            nException = Nothing

        Finally

            nList = Nothing
            nPerson = Nothing
            nCredit = Nothing
            nBrowser = Nothing
            nCharacter = Nothing

        End Try

    End Function

#End Region

#Region " Builders "

    Public Sub New()

        Try

            Me.Initialize()

        Catch nException As Exception

            nException = Nothing

        End Try

    End Sub

#End Region

#Region " Private "

    Private Sub Initialize()

        Try

            Me.id = ""
            Me.name = ""
            Me.category = ""
            Me.image = New Photo
            Me.characters = New List(Of String)

        Catch nException As Exception

            nException = Nothing

        End Try

    End Sub

#End Region

#Region " Methods "

    Public Overrides Function ToString() As String

        ToString = "<Person>"

        Dim nResult As String = Nothing

        Try

            nResult = ""

            If Not Me.name = "" Then nResult = Me.name

            If nResult = "" Then ToString = "<empty>" Else ToString = nResult

        Catch nException As Exception

            nException = Nothing

        Finally

            nResult = Nothing

        End Try

    End Function

    Public Function parseJson(qJObject As JObject) As Boolean

        parseJson = False

        Try



            parseJson = True

        Catch nException As Exception

            nException = Nothing

        Finally

            qJObject = Nothing

        End Try

    End Function

#End Region

#Region " Property "

    Public Property id As String = Nothing
    Public Property name As String = Nothing
    Public Property image As Photo = Nothing
    Public Property category As String = Nothing
    Public Property characters As List(Of String) = Nothing

#End Region

End Class

Public Class Company

#Region " Shared "

    Public Shared Function parseCreditEdge(qJObject As JObject) As List(Of Company)

        parseCreditEdge = New List(Of Company)

        Dim nEdge As JObject = Nothing
        Dim nCompany As Company = Nothing
        Dim nList As List(Of Company) = Nothing

        Try

            nList = New List(Of Company)
            If qJObject IsNot Nothing AndAlso qJObject("edges") IsNot Nothing Then
                For Each nEdge In qJObject("edges")
                    nCompany = New Company
                    If nEdge("node")("company")("id") IsNot Nothing Then nCompany.id = nEdge("node")("company")("id").ToString
                    If nEdge("node")("company")("companyText")("text") IsNot Nothing Then nCompany.name = nEdge("node")("company")("companyText")("text").ToString
                    nList.Add(nCompany)
                Next
            End If

            parseCreditEdge = nList

        Catch nException As Exception

            nException = Nothing

        Finally

            nEdge = Nothing
            nList = Nothing
            nCompany = Nothing

            qJObject = Nothing

        End Try

    End Function

    Public Shared Function parseJsonToList(qBrowser As Browser) As List(Of Company)

        parseJsonToList = New List(Of Company)

        Dim nCompany As Company = Nothing
        Dim nBrowser As Browser = Nothing
        Dim nList As List(Of Company) = Nothing

        Try

            nList = New List(Of Company)

            If qBrowser.Seek("__typename") Then
                Select Case qBrowser.Value
                    Case "CompanyCreditConnection"
                        qBrowser.Seek("edges")
                End Select
            End If

            For Each nBrowser In qBrowser.List
                If nBrowser.Seek("__typename") Then
                    Select Case nBrowser.Value
                        Case "CompanyCreditEdge"
                            nCompany = New Company
                            If nBrowser.Seek("node\company\id") Then nCompany.id = nBrowser.Value
                            If nBrowser.Seek("node\company\companyText\text") Then nCompany.name = nBrowser.Value
                            nList.Add(nCompany)
                    End Select
                End If
            Next

            parseJsonToList = nList

        Catch nException As Exception

            nException = Nothing

        Finally

            nList = Nothing
            nCompany = Nothing
            nBrowser = Nothing

        End Try

    End Function

#End Region

#Region " Builders "

    Public Sub New()

        Try

            Me.Initialize()

        Catch nException As Exception

            nException = Nothing

        End Try

    End Sub

#End Region

#Region " Private "

    Private Sub Initialize()

        Try

            Me.id = ""
            Me.name = ""

        Catch nException As Exception

            nException = Nothing

        End Try

    End Sub

#End Region

#Region " Methods "

    Public Overrides Function ToString() As String

        ToString = "<Company>"

        Dim nResult As String = Nothing

        Try

            nResult = ""

            If Not Me.name = "" Then
                nResult = Me.name
            End If

            If nResult = "" Then ToString = "<empty>" Else ToString = nResult

        Catch nException As Exception

            nException = Nothing

        Finally

            nResult = Nothing

        End Try

    End Function

#End Region

#Region " Property "

    Public Property id As String = Nothing
    Public Property name As String = Nothing

#End Region

End Class

Public Class Country

#Region " Shared "

    Public Shared Function parseCountryOfOrigin(qJObject As JObject) As List(Of Country)

        parseCountryOfOrigin = New List(Of Country)

        Dim nJObject As JObject = Nothing
        Dim nCountry As Country = Nothing
        Dim nList As List(Of Country) = Nothing

        Try

            nList = New List(Of Country)
            If qJObject IsNot Nothing AndAlso qJObject("countries") IsNot Nothing Then
                For Each nJObject In qJObject("countries")
                    nCountry = New Country
                    If nJObject("id") IsNot Nothing Then nCountry.id = nJObject("id").ToString
                    If nJObject("text") IsNot Nothing Then nCountry.name = nJObject("text").ToString
                    nList.Add(nCountry)
                Next
            End If

            parseCountryOfOrigin = nList

        Catch nException As Exception

            nException = Nothing

        Finally

            nList = Nothing
            nJObject = Nothing
            nCountry = Nothing

            qJObject = Nothing

        End Try

    End Function

    Public Shared Function parseJsonToList(qBrowser As Browser) As List(Of Country)

        parseJsonToList = New List(Of Country)

        Dim nCountry As Country = Nothing
        Dim nBrowser As Browser = Nothing
        Dim nList As List(Of Country) = Nothing

        Try

            nList = New List(Of Country)

            If qBrowser.Seek("__typename") Then
                Select Case qBrowser.Value
                    Case "CountriesOfOrigin"
                        qBrowser.Seek("countries")
                End Select
            End If

            For Each nBrowser In qBrowser.List
                If nBrowser.Seek("__typename") Then
                    Select Case nBrowser.Value
                        Case "CountryOfOrigin"
                            nCountry = New Country
                            If nBrowser.Seek("id") Then nCountry.id = nBrowser.Value
                            If nBrowser.Seek("text") Then nCountry.name = nBrowser.Value
                            nList.Add(nCountry)
                    End Select
                End If
            Next

            parseJsonToList = nList

        Catch nException As Exception

            nException = Nothing

        Finally

            nList = Nothing
            nCountry = Nothing
            nBrowser = Nothing

        End Try

    End Function

#End Region

#Region " Builders "

    Public Sub New()

        Try

            Me.Initialize()

        Catch nException As Exception

            nException = Nothing

        End Try

    End Sub

#End Region

#Region " Private "

    Private Sub Initialize()

        Try

            Me.id = ""
            Me.name = ""

        Catch nException As Exception

            nException = Nothing

        End Try

    End Sub

#End Region

#Region " Methods "

    Public Overrides Function ToString() As String

        ToString = "<Country>"

        Dim nResult As String = Nothing

        Try

            nResult = ""

            If Not Me.id = "" Then
                nResult = Me.id
            End If
            If Not Me.name = "" Then
                If nResult.Count > 0 Then nResult = nResult & " - "
                nResult = nResult & Me.name
            End If

            If nResult = "" Then ToString = "<empty>" Else ToString = nResult

        Catch nException As Exception

            nException = Nothing

        Finally

            nResult = Nothing

        End Try

    End Function

#End Region

#Region " Property "

    Public Property id As String = Nothing
    Public Property name As String = Nothing

#End Region

End Class

Public Class Genre

#Region " Shared "

    Public Shared Function parseGenres(qJObject As JObject) As List(Of Genre)

        parseGenres = New List(Of Genre)

        Dim nGenre As Genre = Nothing
        Dim nJObject As JObject = Nothing
        Dim nList As List(Of Genre) = Nothing

        Try

            nList = New List(Of Genre)
            If qJObject IsNot Nothing AndAlso qJObject("genres") IsNot Nothing Then
                For Each nJObject In qJObject("genres")
                    nGenre = New Genre
                    If nJObject("id") IsNot Nothing Then nGenre.id = nJObject("id").ToString
                    If nJObject("text") IsNot Nothing Then nGenre.name = nJObject("text").ToString
                    nList.Add(nGenre)
                Next
            End If

            parseGenres = nList

        Catch nException As Exception

            nException = Nothing

        Finally

            nList = Nothing
            nGenre = Nothing
            nJObject = Nothing

            qJObject = Nothing

        End Try

    End Function

    Public Shared Function parseJsonToList(qBrowser As Browser) As List(Of Genre)

        parseJsonToList = New List(Of Genre)

        Dim nGenre As Genre = Nothing
        Dim nBrowser As Browser = Nothing
        Dim nList As List(Of Genre) = Nothing

        Try

            nList = New List(Of Genre)

            If qBrowser.Seek("__typename") Then
                Select Case qBrowser.Value
                    Case "Genres"

                        qBrowser.Seek("genres")

                        For Each nBrowser In qBrowser.List
                            nGenre = New Genre
                            If nBrowser.Seek("id") Then nGenre.id = nBrowser.Value
                            If nBrowser.Seek("text") Then nGenre.name = nBrowser.Value
                            nList.Add(nGenre)
                        Next

                    Case "TitleGenres"

                        qBrowser.Seek("genres")

                        For Each nBrowser In qBrowser.List
                            nGenre = New Genre
                            If nBrowser.Seek("genre\text") Then nGenre.id = nBrowser.Value
                            If nBrowser.Seek("genre\text") Then nGenre.name = nBrowser.Value
                            nList.Add(nGenre)
                        Next

                End Select
            End If



            parseJsonToList = nList

        Catch nException As Exception

            nException = Nothing

        Finally

            nList = Nothing
            nGenre = Nothing
            nBrowser = Nothing

        End Try

    End Function

#End Region

#Region " Builders "

    Public Sub New()

        Try

            Me.Initialize()

        Catch nException As Exception

            nException = Nothing

        End Try

    End Sub

#End Region

#Region " Private "

    Private Sub Initialize()

        Try

            Me.id = ""
            Me.name = ""

        Catch nException As Exception

            nException = Nothing

        End Try

    End Sub

#End Region

#Region " Methods "

    Public Overrides Function ToString() As String

        ToString = "<Genre>"

        Dim nResult As String = Nothing

        Try

            nResult = ""

            If Not Me.name = "" Then
                nResult = Me.name
            End If

            If nResult = "" Then ToString = "<empty>" Else ToString = nResult

        Catch nException As Exception

            nException = Nothing

        Finally

            nResult = Nothing

        End Try

    End Function

#End Region

#Region " Property "

    Public Property id As String = Nothing
    Public Property name As String = Nothing

#End Region

End Class

Public Class Language

#Region " Shared "

    Public Shared Function parseSpokenLanguage(qJObject As JObject) As List(Of Language)

        parseSpokenLanguage = New List(Of Language)

        Dim nJObject As JObject = Nothing
        Dim nLanguage As Language = Nothing
        Dim nList As List(Of Language) = Nothing

        Try

            nList = New List(Of Language)
            If qJObject IsNot Nothing AndAlso qJObject("spokenLanguages") IsNot Nothing Then
                For Each nJObject In qJObject("spokenLanguages")
                    nLanguage = New Language
                    If nJObject("id") IsNot Nothing Then nLanguage.id = nJObject("id").ToString
                    If nJObject("text") IsNot Nothing Then nLanguage.name = nJObject("text").ToString
                    nList.Add(nLanguage)
                Next
            End If

            parseSpokenLanguage = nList

        Catch nException As Exception

            nException = Nothing

        Finally

            nList = Nothing
            nJObject = Nothing
            nLanguage = Nothing

            qJObject = Nothing

        End Try

    End Function

    Public Shared Function parseJsonToList(qBrowser As Browser) As List(Of Language)

        parseJsonToList = New List(Of Language)

        Dim nBrowser As Browser = Nothing
        Dim nLanguage As Language = Nothing
        Dim nList As List(Of Language) = Nothing

        Try

            nList = New List(Of Language)

            If qBrowser.Seek("__typename") Then
                Select Case qBrowser.Value
                    Case "SpokenLanguages"
                        qBrowser.Seek("spokenLanguages")
                End Select
            End If

            For Each nBrowser In qBrowser.List
                If nBrowser.Seek("__typename") Then
                    Select Case nBrowser.Value
                        Case "SpokenLanguage"
                            nLanguage = New Language
                            If nBrowser.Seek("id") Then nLanguage.id = nBrowser.Value
                            If nBrowser.Seek("text") Then nLanguage.name = nBrowser.Value
                            nList.Add(nLanguage)
                    End Select
                End If
            Next

            parseJsonToList = nList

        Catch nException As Exception

            nException = Nothing

        Finally

            nList = Nothing
            nBrowser = Nothing
            nLanguage = Nothing

        End Try

    End Function

#End Region

#Region " Builders "

    Public Sub New()

        Try

            Me.Initialize()

        Catch nException As Exception

            nException = Nothing

        End Try

    End Sub

#End Region

#Region " Private "

    Private Sub Initialize()

        Try

            Me.id = ""
            Me.name = ""

        Catch nException As Exception

            nException = Nothing

        End Try

    End Sub

#End Region

#Region " Methods "

    Public Overrides Function ToString() As String

        ToString = "<Language>"

        Dim nResult As String = Nothing

        Try

            nResult = ""

            If Not Me.id = "" Then
                nResult = Me.id
            End If
            If Not Me.name = "" Then
                If nResult.Count > 0 Then nResult = nResult & " - "
                nResult = nResult & Me.name
            End If

            If nResult = "" Then ToString = "<empty>" Else ToString = nResult

        Catch nException As Exception

            nException = Nothing

        Finally

            nResult = Nothing

        End Try

    End Function

#End Region

#Region " Property "

    Public Property id As String = Nothing
    Public Property name As String = Nothing

#End Region

End Class

Public Class Rating

#Region " Builders "

    Public Sub New()

        Try

            Me.Initialize()

        Catch nException As Exception

            nException = Nothing

        End Try

    End Sub

#End Region

#Region " Private "

    Private Sub Initialize()

        Try

            Me.voteCount = ""
            Me.difference = ""
            Me.currentRank = ""
            Me.changeDirection = ""
            Me.aggregateRating = ""

        Catch nException As Exception

            nException = Nothing

        End Try

    End Sub

#End Region

#Region " Methods "

    Public Overrides Function ToString() As String

        ToString = "<Rating>"

        Dim nResult As String = Nothing

        Try

            nResult = ""

            If Not Me.aggregateRating = "" Then
                nResult = Me.aggregateRating & "/10"
            End If

            If Not Me.voteCountText = "" Then
                If nResult.Count > 0 Then nResult = nResult & " ("
                nResult = nResult & Me.voteCountText & ") Then"
            End If

            If Not Me.currentRank = "" Then
                If nResult.Count > 0 Then nResult = nResult & " "
                nResult = nResult & "Popularity " & Me.currentRank
            End If

            If Not Me.difference = "" Then
                If nResult.Count > 0 Then nResult = nResult & " "
                If Me.changeDirection = "DOWN" Then
                    nResult = nResult & "(-" & Me.difference & ")"
                Else
                    nResult = nResult & "(+" & Me.difference & ")"
                End If
            End If

            If nResult = "" Then ToString = "<empty>" Else ToString = nResult

        Catch nException As Exception

            nException = Nothing

        Finally

            nResult = Nothing

        End Try

    End Function

#End Region

#Region " Property "

    Public Property voteCount As String = Nothing
    Public Property difference As String = Nothing
    Public Property currentRank As String = Nothing
    Public Property changeDirection As String = Nothing
    Public Property aggregateRating As String = Nothing
    Public Property voteCountText As String

        Get

            voteCountText = ""

            Dim nIndex As Integer = Nothing
            Dim nNumber As Double = Nothing
            Dim nUnits() As String = Nothing

            Try

                nUnits = {"", "K", "M", "B", "T", "Q"}

                If Double.TryParse(Me.voteCount, nNumber) Then
                    nIndex = 0
                    While nNumber >= 1000 AndAlso nIndex < nUnits.Length - 1
                        nNumber /= 1000
                        nIndex += 1
                    End While
                    voteCountText = String.Format("{00.0}{1}", nNumber, nUnits(nIndex))
                End If

            Catch nException As Exception

                nException = Nothing

            Finally

                nIndex = Nothing
                nUnits = Nothing
                nNumber = Nothing

            End Try

        End Get

        Set(value As String)

            Try

            Catch nException As Exception

                nException = Nothing

            Finally

                value = Nothing

            End Try

        End Set

    End Property

#End Region

End Class

Public Class BoxOffice

#Region " Builders "

    Public Sub New()

        Try

            Me.Initialize()

        Catch nException As Exception

            nException = Nothing

        End Try

    End Sub

#End Region

#Region " Private "

    Private Sub Initialize()

        Try

            Me.lifetimeGross = New Money
            Me.worldwideGross = New Money
            Me.productionBudget = New Money
            Me.openingWeekendGross = New Money

        Catch nException As Exception

            nException = Nothing

        End Try

    End Sub

#End Region

#Region " Methods "

    Public Overrides Function ToString() As String

        ToString = "<boxOffice>"

        Dim nResult As String = Nothing

        Try

            nResult = Me.worldwideGross.ToString
            If nResult = "" Then
                nResult = "<empty>"
            Else
                nResult = "Gross worldwide " & nResult
            End If

            ToString = nResult

        Catch nException As Exception

            nException = Nothing

        Finally

            nResult = Nothing

        End Try

    End Function

#End Region

#Region " Proprietà "

    Public Property lifetimeGross As Money = Nothing
    Public Property worldwideGross As Money = Nothing
    Public Property productionBudget As Money = Nothing
    Public Property openingWeekendGross As Money = Nothing

#End Region

End Class

Public Class Money

#Region " Builders "

    Public Sub New()

        Try

            Me.Initialize()

        Catch nException As Exception

            nException = Nothing

        End Try

    End Sub

#End Region

#Region " Private "

    Private Sub Initialize()

        Try

            Me.amount = ""
            Me.currency = ""

        Catch nException As Exception

            nException = Nothing

        End Try

    End Sub

#End Region

#Region " Methods "

    Public Overrides Function ToString() As String

        ToString = "<Money>"

        Dim nResult As String = Nothing

        Try

            nResult = ""
            nResult = Me.amountText

            ToString = nResult

        Catch nException As Exception

            nException = Nothing

        Finally

            nResult = Nothing

        End Try

    End Function

#End Region

#Region " Property "

    Public Property amount As String = Nothing
    Public Property currency As String = Nothing
    Public Property amountText As String

        Get

            amountText = ""

            Dim nNumber As Double = Nothing

            Try

                If Double.TryParse(Me.amount, nNumber) Then
                    amountText = String.Format("{0N0}", nNumber)
                    Select Case Me.currency
                        Case "USD"
                            amountText = "$" & amountText
                        Case Else
                            amountText = "€" & amountText
                    End Select
                End If

            Catch nException As Exception

                nException = Nothing

            Finally

                nNumber = Nothing

            End Try

        End Get

        Set(value As String)

            Try

            Catch nException As Exception

                nException = Nothing

            Finally

                value = Nothing

            End Try

        End Set

    End Property

#End Region

End Class

Public Class Episodes

#Region " Builders "

    Public Sub New()

        Try

            Me.Initialize()

        Catch nException As Exception

            nException = Nothing

        End Try

    End Sub

#End Region

#Region " Private "

    Private Sub Initialize()

        Try

            Me.total = ""
            Me.seasons = ""

        Catch nException As Exception

            nException = Nothing

        End Try

    End Sub

#End Region

#Region " Methods "

    Public Overrides Function ToString() As String

        ToString = "<Episodes>"

        Dim nResult As String = Nothing

        Try

            nResult = ""
            If Me.total = "" AndAlso Me.seasons = "" Then
                nResult = "<empty>"
            Else
                nResult = "Seasons: " & Me.seasons & " Episodes: " & Me.total
            End If

            ToString = nResult

        Catch nException As Exception

            nException = Nothing

        Finally

            nResult = Nothing

        End Try

    End Function

    Public Function parseJson(qBrowser As Browser) As Boolean

        parseJson = False

        Try

            If qBrowser.Seek("episodes\total") Then Me.total = qBrowser.Value : parseJson = True
            If qBrowser.Seek("seasons") Then Me.seasons = qBrowser.List.Count.ToString : parseJson = True

        Catch nException As Exception

            nException = Nothing

        Finally

            qBrowser = Nothing

        End Try

    End Function

#End Region

#Region " Property "

    Public Property total As String = Nothing
    Public Property seasons As String = Nothing

#End Region

End Class

Public Class PlaybackURL

#Region " Builders "

    Public Sub New()

        Try

            Me.Initialize()

        Catch nException As Exception

            nException = Nothing

        End Try

    End Sub

#End Region

#Region " Private "

    Private Sub Initialize()

        Try

            Me.quality = ""
            Me.language = ""
            Me.mimeType = ""
            Me.url = ""

        Catch nException As Exception

            nException = Nothing

        End Try

    End Sub

#End Region

#Region " Methods "

    Public Overrides Function ToString() As String

        ToString = "<PlaybackURLs>"

        Try

            If Me.url = "" Then
                ToString = "<empty>"
            Else
                ToString = Me.url
            End If

        Catch nException As Exception

            nException = Nothing

        End Try

    End Function

    Public Function parseJson(qBrowser As Browser) As Boolean

        parseJson = False

        Try

            If qBrowser.Seek("__typename") Then
                Select Case qBrowser.Value
                    Case "PlaybackURL"
                        If qBrowser.Seek("displayName\value") Then Me.quality = qBrowser.Value
                        If qBrowser.Seek("displayName\language") Then Me.language = qBrowser.Value
                        If qBrowser.Seek("mimeType") Then Me.mimeType = qBrowser.Value
                        If qBrowser.Seek("url") Then Me.url = qBrowser.Value
                        parseJson = True
                End Select
            End If

        Catch nException As Exception

            nException = Nothing

        Finally

            qBrowser = Nothing

        End Try

    End Function

#End Region

#Region " Property "

    Public Property quality As String = Nothing
    Public Property language As String = Nothing
    Public Property mimeType As String = Nothing
    Public Property url As String = Nothing

#End Region

End Class

Public Class Extractor

#Region " Shared "

    Public Shared Function getListOfString(qJObject As JObject) As List(Of String)

        getListOfString = New List(Of String)

        Dim nValue As String = Nothing
        Dim nJObject As JObject = Nothing
        Dim nList As List(Of String) = Nothing

        Try

            nList = New List(Of String)

            If (qJObject IsNot Nothing AndAlso qJObject("__typename") IsNot Nothing) Then
                Select Case qJObject("__typename").ToString
                    Case "Genres"
                        For Each nJObject In qJObject("genres")
                            nValue = nJObject("text").ToString
                            nList.Add(nValue)
                        Next
                    Case "TitleKeywordConnection"
                        For Each nJObject In qJObject("edges")
                            If nJObject("node")("text") IsNot Nothing Then
                                nValue = nJObject("node")("text").ToString
                                nList.Add(nValue)
                            End If
                        Next
                End Select
            End If

            getListOfString = nList

        Catch nException As Exception

            nException = Nothing

        Finally

            nList = Nothing
            nValue = Nothing
            nJObject = Nothing

            qJObject = Nothing

        End Try

    End Function

    Public Shared Function getListOfString(qBrowser As Browser) As List(Of String)

        getListOfString = New List(Of String)

        Dim nValue As String = Nothing
        Dim nBrowser As Browser = Nothing
        Dim nList As List(Of String) = Nothing

        Try

            nList = New List(Of String)

            If qBrowser.Seek("__typename") Then
                Select Case qBrowser.Value
                    Case "Genres"
                        If qBrowser.Seek("genres") Then
                            For Each nBrowser In qBrowser.List
                                If nBrowser.Seek("text") Then
                                    nValue = nBrowser.Value
                                    nList.Add(nValue)
                                End If
                            Next
                        End If
                    Case "TitleKeywordConnection"
                        If qBrowser.Seek("edges") Then
                            For Each nBrowser In qBrowser.List
                                If nBrowser.Seek("node\text") Then
                                    nValue = nBrowser.Value
                                    nList.Add(nValue)
                                End If
                            Next
                        End If
                End Select
            End If

            getListOfString = nList

        Catch nException As Exception

            nException = Nothing

        Finally

            nList = Nothing
            nValue = Nothing
            nBrowser = Nothing

            qBrowser = Nothing

        End Try

    End Function


#End Region

End Class

Public Class Browser

#Region " Protected "

    Protected pSeek As JToken = Nothing
    Protected pRoot As JToken = Nothing

#End Region

#Region " Builders "

    Public Sub New(qJToken As JToken)

        Try

            Me.Initialize()

            pSeek = qJToken
            pRoot = qJToken

        Catch nException As Exception

            nException = Nothing

        Finally

            qJToken = Nothing

        End Try

    End Sub

    Public Sub New(qJson As String)

        Dim nJObject As JObject = Nothing

        Try

            Me.Initialize()

            nJObject = JObject.Parse(qJson)

            pSeek = nJObject
            pRoot = nJObject

        Catch nException As Exception

            nException = Nothing

        Finally

            nJObject = Nothing

            qJson = Nothing

        End Try

    End Sub

#End Region

#Region " Private "

    Private Sub Initialize()

        Try

            pSeek = Nothing
            pRoot = Nothing

        Catch nException As Exception

            nException = Nothing

        End Try

    End Sub

#End Region

#Region " Methods "

    Public Function Seek(qPath As String) As Boolean

        Seek = False

        Dim nKey As String = Nothing
        Dim nChars() As Char = Nothing
        Dim nJToken As JToken = Nothing
        Dim nFound As Boolean = Nothing
        Dim nSplit() As String = Nothing

        Try

            If qPath IsNot Nothing Then
                nChars = {"\"c}
                nSplit = qPath.Split(nChars, StringSplitOptions.RemoveEmptyEntries)
                If nSplit.Length > 0 Then
                    nFound = True
                    nJToken = pRoot
                    For Each nKey In nSplit
                        If nJToken.HasValues Then
                            nJToken = nJToken(nKey)
                            If nJToken Is Nothing Then nFound = False : Exit For
                        End If
                    Next
                    If nFound Then pSeek = nJToken : Seek = True
                End If
            End If

        Catch nException As Exception

            nException = Nothing

        Finally

            nKey = Nothing
            nChars = Nothing
            nFound = Nothing
            nSplit = Nothing
            nJToken = Nothing

            qPath = Nothing

        End Try

    End Function

#End Region

#Region " Property "

    Public ReadOnly Property Token As JToken

        Get

            Token = Nothing

            Try

                Token = pSeek

            Catch nException As Exception

                nException = Nothing

            End Try

        End Get

    End Property

    Public ReadOnly Property Value As String

        Get

            Value = ""

            Try

                If pSeek IsNot Nothing Then Value = pSeek.ToString

            Catch nException As Exception

                nException = Nothing

            End Try

        End Get

    End Property

    Public ReadOnly Property Truncate As Browser

        Get

            Truncate = Nothing

            Try

                Truncate = New Browser(pSeek)

            Catch nException As Exception

                nException = Nothing

            End Try

        End Get

    End Property

    Public ReadOnly Property List As List(Of Browser)

        Get

            List = New List(Of Browser)

            Dim nBrowser As Browser = Nothing
            Dim nJObject As JObject = Nothing
            Dim nArray As List(Of Browser) = Nothing

            Try

                nArray = New List(Of Browser)
                If pSeek IsNot Nothing Then
                    For Each nJObject In pSeek
                        nBrowser = New Browser(nJObject)
                        nArray.Add(nBrowser)
                    Next
                End If
                List = nArray

            Catch nException As Exception

                nException = Nothing

            Finally

                nArray = Nothing
                nBrowser = Nothing
                nJObject = Nothing

            End Try

        End Get

    End Property

    Public ReadOnly Property Values As List(Of String)

        Get

            Values = New List(Of String)

            Dim nJValue As JValue = Nothing
            Dim nArray As List(Of String) = Nothing

            Try

                nArray = New List(Of String)
                If pSeek IsNot Nothing Then
                    For Each nJValue In pSeek
                        nArray.Add(nJValue.ToString)
                    Next
                End If
                Values = nArray

            Catch nException As Exception

                nException = Nothing

            Finally

                nArray = Nothing
                nJValue = Nothing

            End Try

        End Get

    End Property

#End Region

End Class

Public Enum eSearch As Integer
    All = 0
    Titles = 1
    Episodes = 2
    Celebs = 3
    Companies = 4
    Keywords = 5
End Enum

Public Enum eLanguage As Integer
    en = 0
    it = 1
    fr = 2
End Enum

