import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { NgbActiveModal, NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { Disease } from '../../models';
import { DeletemodalComponent } from './deletemodal/deletemodal.component';

@Component({
  selector: 'app-diseasetable',
  templateUrl: './diseasetable.component.html',
  providers: [NgbActiveModal]
})
export class DiseasetableComponent implements OnInit {
  @Input() vm: any;
  @Output() clickDeleteDisease: EventEmitter<number> = new EventEmitter;
  @Input() error: String | null = null;

  constructor(private ms: NgbModal, private am: NgbActiveModal) { }

  ngOnInit(): void {
  }

  deleteDisease(disease: Disease) {
    if (disease.id == NaN || disease.id == undefined || disease.id == null) {
      return;
    }
    this.clickDeleteDisease.emit(disease.id);
  }

  openModal(disease: Disease) {
    const modalRef = this.ms.open(DeletemodalComponent, {centered: true} );
    modalRef.componentInstance.disease = disease;
    modalRef.result.then(
      (result) => { this.deleteDisease(disease); },
      (reason) => { /* Don't do anything in particular */ }
    );
  }

}
