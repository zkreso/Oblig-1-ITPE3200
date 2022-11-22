import { Component, Input, OnInit } from '@angular/core';
import { NgbActiveModal, NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { Disease } from '../../../models';

@Component({
  selector: 'app-deletemodal',
  templateUrl: './deletemodal.component.html'
})
export class DeletemodalComponent implements OnInit {
  @Input() disease!: Disease;

  constructor(public am: NgbActiveModal) { }

  ngOnInit(): void {
  }

}
