import { Component, ElementRef, ChangeDetectionStrategy } from '@angular/core';
import { NgForm, FormsModule } from '@angular/forms';

@Component({
  selector: 'app-purchase',
  templateUrl: './purchase.html',
  changeDetection: ChangeDetectionStrategy.Eager,
  styleUrl: './purchase.scss',
  imports: [FormsModule],
})
export class Purchase {
  selectedFile!: File;
  submitUploadStatus(form: NgForm) {
    alert(form.value.statusFile);
  }
  submitPurchase(form: NgForm) {
    if (!this.selectedFile) {
      console.log('No file selected');
      return;
    }

    const formData = new FormData();
    formData.append('file', this.selectedFile);

    alert(this.selectedFile.name);
  }

  onFileSelected(event: any) {
    const input = event.target as HTMLInputElement;

    if (input.files && input.files.length > 0) {
      this.selectedFile = input.files[0];
    }
  }
}
