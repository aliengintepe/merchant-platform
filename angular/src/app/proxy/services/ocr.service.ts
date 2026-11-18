import { RestService, Rest } from '@abp/ng.core';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class OcrService {
  apiName = 'Default';
  

  extractTextByImagePath = (imagePath: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, string>({
      method: 'POST',
      responseType: 'text',
      url: '/api/app/ocr/extract-text',
      params: { imagePath },
    },
    { apiName: this.apiName,...config });

  constructor(private restService: RestService) {}
}
