import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { NgbActiveModal, NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { Subscription } from 'rxjs';
import { Disease } from '../../models';
import { LoginService } from '../../services/login.service';
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

  public loggedIn = false;
  private subscription!: Subscription;

  constructor(private ms: NgbModal, private ls: LoginService) { }

  ngOnInit(): void {
    this.subscription = this.ls.isAuthenticated().subscribe(res => this.loggedIn = res);
  }

  ngOnDestroy(): void {
    this.subscription.unsubscribe();
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
