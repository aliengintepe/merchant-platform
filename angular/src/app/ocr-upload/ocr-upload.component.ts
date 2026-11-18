import { Component } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-ocr-upload',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './ocr-upload.component.html',
  styleUrls: ['./ocr-upload.component.scss']
})
export class OcrUploadComponent {
  selectedFile: File | null = null;
  ocrLabel: string = '';
  ocrAccuracy: number = 0;
  tcKimlikNo: string = ''; 

  constructor(private http: HttpClient) {}

  onFileSelected(event: any) {
    const file = event.target.files[0];
    if (file) {
      this.selectedFile = file;
      console.log('Seçilen dosya:', file.name);
    }
  }

  onUpload() {
    if (!this.selectedFile) {
      alert('Lütfen bir dosya seçin.');
      return;
    }

    const formData = new FormData();
    formData.append('File', this.selectedFile);

    const token = localStorage.getItem('access_token') || '';
    const headers = new HttpHeaders({
      'Authorization': `Bearer ${token}`
    });

    this.http.post<any>('https://localhost:44380/api/app/ocr/upload', formData, { headers })
      .subscribe({
        next: (response) => {
          console.log('ML Yanıtı:', response);

          this.ocrLabel = response?.label || 'Belirlenemedi';
          this.ocrAccuracy = +(response?.accuracy * 100).toFixed(2) || 0;
          this.tcKimlikNo = response?.tcKimlikNo || ''; 
        },
        error: (err) => {
          console.error('Yükleme hatası:', err);
          this.ocrLabel = 'Hata oluştu';
          this.ocrAccuracy = 0;
          this.tcKimlikNo = '';
        }
      });
  }
}
