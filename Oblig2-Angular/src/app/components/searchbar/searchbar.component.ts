import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormControl } from '@angular/forms';
import { debounceTime, distinctUntilChanged, map } from 'rxjs';

@Component({
  selector: 'app-searchbar',
  templateUrl: './searchbar.component.html',
  styleUrls: ['./searchbar.component.css']
})
export class SearchbarComponent implements OnInit {
  @Output() clickSearch: EventEmitter<string> = new EventEmitter;
  @Output() clickClear: EventEmitter<void> = new EventEmitter;
  @Input() label?: string;
  @Input() placeholder?: string;

  searchStringControl = new FormControl("");

  @Output() search$ = this.searchStringControl.valueChanges.pipe(
    map(val => val ??= ""),
    debounceTime(400),
    distinctUntilChanged()
  );

  constructor() { }

  ngOnInit(): void {
  }

  search(): void {
    if (this.searchStringControl.value == "" || this.searchStringControl.value == null) {
      return;
    }
    this.clickSearch.emit(this.searchStringControl.value);
  }

  clear(): void {
    this.searchStringControl.setValue("");
    this.clickClear.emit();
  }

}
