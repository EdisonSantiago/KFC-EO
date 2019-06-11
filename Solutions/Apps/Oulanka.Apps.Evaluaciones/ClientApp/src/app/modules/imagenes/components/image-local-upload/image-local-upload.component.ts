import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { FormGroup, FormBuilder } from '@angular/forms';
import { ImagenesService } from '../../services/imagenes.service';
import * as $ from 'jquery';
import { ImagenLocal } from '../../domain/imagenLocal';
import { Guid } from 'guid-typescript';
import { NotificationService } from 'src/app/core/services/notification.service';

@Component({
  selector: 'app-image-local-upload',
  templateUrl: './image-local-upload.component.html',
})
export class ImageLocalUploadComponent implements OnInit {

  @Input() localId: any;
  @Output() uploaded = new EventEmitter<boolean>();

  form: FormGroup;
  error: string;
  uploadResponse = {status: '', message: '', filePath: ''};

  constructor(
    private formBuilder: FormBuilder,
    private imageService: ImagenesService,
    private notificationService: NotificationService
  ) { }

  ngOnInit() {
    this.form = this.formBuilder.group({localImg: [] });
  }

  onFileChange(event) {
    if (event.target.files.length > 0) {
      const file = event.target.files[0];
      this.form.get('localImg').setValue(file);

      const reader = new FileReader();
      reader.onload = (e: any) => {
        const imagePreview = document.getElementById('image-preview');
        $('#image-preview').attr('src', e.target.result);
        this.storeTheImage();
      };
      reader.readAsDataURL(file);
    }
  }

  saveImageToIDB(localId: any, data: any) {
    const imagenLocal: ImagenLocal = new ImagenLocal();
    imagenLocal.id = Guid.create().toString();
    imagenLocal.imagenData = data;
    imagenLocal.localId = localId;

    this.imageService.saveLocalImage(imagenLocal).then( res => {
      this.imageService.saveToApi(localId);
      this.notificationService.printSuccessMessage('imagen cargada ok!');
      this.uploaded.emit(true);
    });
  }

  storeTheImage() {
    const img: any = document.getElementById('image-preview');
    console.log(img.src);
    this.saveImageToIDB(this.localId, img.src);
  }


}
