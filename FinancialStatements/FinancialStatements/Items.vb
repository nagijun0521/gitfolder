Public Class Items

    '初期項目配列
    ''' <summary>流動資産(項目名、金額)</summary>
    Private DefLiquidAsset(,) As String = {{"現金", "0"}, {"預貯金", "0"}, {"保険", "0"}, {"株", "0"}}
    ''' <summary>固定資産(項目名、金額)</summary>
    Private DefFixedAsset(,) As String = {{"土地", "0"}, {"建物", "0"}, {"自動車", "0"}}
    ''' <summary>負債(項目名、金額)</summary>
    Private DefLiability(,) As String = {{"住宅ローン", "0"}, {"自動車ローン", "0"}, {"その他負債", "0"}}

    ''' <summary>
    ''' 各資産の項目名を返すメソッド
    ''' </summary>
    ''' <param name="assetName">資産名列挙体。流動資産、固定資産、負債</param>
    Public Function LoadDefItems(ByVal assetName As AssetName) As String()
        Select Case assetName
            Case assetName.LiquidAsset
                Dim St(DefLiquidAsset.GetLength(0) - 1) As String
                For i As Integer = 0 To DefLiquidAsset.GetLength(0) - 1
                    St(i) = DefLiquidAsset(i, 0)
                Next
                Return St
            Case assetName.FixidAsset
                Dim St(DefFixedAsset.GetLength(0) - 1) As String
                For i As Integer = 0 To DefFixedAsset.GetLength(0) - 1
                    St(i) = DefFixedAsset(i, 0)
                Next
                Return St
            Case assetName.Liability
                Dim St(DefLiability.GetLength(0) - 1) As String
                For i As Integer = 0 To DefLiability.GetLength(0) - 1
                    St(i) = DefLiability(i, 0)
                Next
                Return St
        End Select

        Return Nothing
    End Function


End Class
