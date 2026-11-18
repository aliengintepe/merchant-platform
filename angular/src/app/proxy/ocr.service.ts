import type { UploadFileDto } from './dtos/models';
import type { IActionResult } from './microsoft/asp-net-core/mvc/models';
import { RestService, Rest } from '@abp/ng.core';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class OcrService {
  apiName = 'Default';
  

  upload = (input: UploadFileDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, IActionResult>({
      method: 'POST',
      url: '/api/app/ocr/upload',
      body: input.file,
    },
    { apiName: this.apiName,...config });

  constructor(private restService: RestService) {}
}
