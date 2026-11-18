import { NgModule } from '@angular/core';
import { SharedModule } from '../shared/shared.module';
import { HomeRoutingModule } from './home-routing.module';
import { HomeComponent } from './home.component';
import { OcrUploadComponent } from '../ocr-upload/ocr-upload.component';
import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
@NgModule({
  declarations: 
  [
    HomeComponent,
   
  ],
  imports: [
    SharedModule,
    CommonModule,
    HttpClientModule,
    HomeRoutingModule,
    OcrUploadComponent
    ],
})
export class HomeModule {
  
}
