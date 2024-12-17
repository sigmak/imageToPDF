Imports GemBox.Pdf
Imports GemBox.Pdf.Content
Imports System.IO

Public Class Form1
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        ' GemBox.Pdf 라이선스 키 설정
        ComponentInfo.SetLicense("FREE-LIMITED-KEY")

        Dim openFileDialog As New OpenFileDialog()
        openFileDialog.Filter = "TIFF Files|*.tif;*.tiff"
        openFileDialog.Title = "Select a TIFF Image File"

        If openFileDialog.ShowDialog() = DialogResult.OK Then
            Dim imagePath As String = openFileDialog.FileName
            Dim pdfPath As String = Path.ChangeExtension(imagePath, "pdf")

            Try
                ' PDF 문서 생성
                Using document As New PdfDocument()
                    ' 이미지 로드
                    Dim pdfImage = GemBox.Pdf.Content.PdfImage.Load(imagePath)

                    Dim widthInPoints As Double = pdfImage.Width 'pdfImage.Width * 72 / 96
                    Dim heightInPoints As Double = pdfImage.Height 'pdfImage.Height * 72 / 96
                    ' 페이지 추가 및 크기 설정
                    Dim page = document.Pages.Add()
                    'page.SetMediaBox(0, 0, widthInPoints, heightInPoints)
                    'page.SetMediaBox(-widthInPoints, -heightInPoints, widthInPoints, heightInPoints) '이거 사용하면 사이즈가 많이 작아짐????

                    ' 이미지를 페이지에 그리기
                    page.Content.DrawImage(pdfImage, New PdfPoint(0, 0))

                    ' PDF 저장
                    document.Save(pdfPath)

                    MessageBox.Show($"PDF 파일이 저장되었습니다: {pdfPath}", "완료", MessageBoxButtons.OK, MessageBoxIcon.Information)
                End Using

            Catch ex As Exception
                MessageBox.Show($"오류 발생: {ex.Message}", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If
    End Sub
End Class