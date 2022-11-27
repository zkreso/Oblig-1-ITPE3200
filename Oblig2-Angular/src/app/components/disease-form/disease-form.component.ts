import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { DiseaseEntity } from '../../models';

@Component({
  selector: 'app-disease-form',
  templateUrl: './disease-form.component.html'
})
export class DiseaseFormComponent implements OnInit {
  @Input() success!: boolean;
  @Input() errorMessage: any;
  @Input() successString?: string;
  @Output() clickSubmit: EventEmitter<DiseaseEntity> = new EventEmitter;

  form = this.fb.group({
    name: ['', [Validators.required, Validators.pattern("[a-zA-ZæøåÆØÅ0-9\\\'\"\(\)-. ]{1,}")]],
    description: ['', [Validators.nullValidator, Validators.pattern("[a-zA-ZæøåÆØÅ0-9\\\'\"\(\)-. ]*")]]
  });

  constructor(private fb: FormBuilder) { }

  ngOnInit(): void { }

  submit() {
    if (this.form.invalid) {
      return;
    }
    this.form.markAsUntouched();
    this.clickSubmit.emit({
      name: this.form.value.name!,
      description: this.form.value.description!
    });
  }

}
