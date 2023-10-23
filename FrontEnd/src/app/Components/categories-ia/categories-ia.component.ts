import { Component, ElementRef, Injectable, OnChanges, OnInit, SimpleChanges, ViewChild } from '@angular/core';
import { NavBarComponent } from '../nav-bar/nav-bar.component';
import { BehaviorSubject, delay, of } from 'rxjs';
import { FormBuilder, FormControl } from '@angular/forms';
import { CategoriesAIService } from '../../Services/categories-ai.service';
import { environment } from "../../../environments/environment";

export interface categories {
  id: number;
  title: string;
  image: string;
  description: string;
}

declare let M: any;

@Injectable()
export class DataSharingService {
    public isUserLoggedIn: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(false);
}

@Component({
  selector: 'app-categories-ia',
  templateUrl: './categories-ia.component.html',
  styleUrls: ['./categories-ia.component.css']
})

export class CategoriesIAComponent implements OnInit{ 

  @ViewChild('textarea') textareaRef: ElementRef | undefined;
  @ViewChild('title') titleElement: ElementRef | undefined;
  
  constructor(private navBarComponent: NavBarComponent, 
              private formBuilder: FormBuilder,
              private categoriesAIService: CategoriesAIService
  ) {}

  inputTitle: string | undefined;
  inputDescription: string | undefined;
  textareaCols: number | undefined;
  formForSubmit: any;
  nameSelected: string | undefined;
  IdSelected: string | undefined;
  UserLogin: any;
  roleId: number | undefined;
  file: any;
  Categories: any;
  Category: any;
  url: string = environment.url_BackEnd;


  ngOnInit(): void {

    this.categoriesAIService.getCategories();

    document.addEventListener('DOMContentLoaded', function() {
      const elems = document.querySelectorAll('.modal');
      const instances = M.Modal.init(elems);
    });

    if (localStorage.getItem("token")) {
      this.navBarComponent.refreshComponent();
    }

    if (localStorage.getItem("Login")) {
      
      this.UserLogin = localStorage.getItem('Login');

      this.UserLogin = JSON.parse(this.UserLogin);

      this.roleId! = this.UserLogin.roleId;
    }

    this.Categories = localStorage.getItem('CategoriesList');

    this.Categories = JSON.parse(this.Categories);

    this.formForSubmit = this.formBuilder.group({
      Title: '',
      Description: '',
      Image: new FormControl(null),
    });

  }

  nameSelect(name:string, Id:string){

    this.nameSelected = name;
    this.IdSelected = Id;
    
  }

  idCategory(Id:string, name:string){
    localStorage.setItem("IdCategory", Id);
    localStorage.setItem("NameCategory", name);
  }

  HiddenBtn(variable1:any, variable2:any){
    let btnAdd = document.getElementById("CategoriesAdd");
    btnAdd!.style.display = variable1;
    

    let btnSave = document.getElementById("CategoriesSave");
    btnSave!.style.display = variable2;

    this.inputTitle = "";
    this.inputDescription = "";

  }

  async editCategoty(Id:string){
    this.categoriesAIService.getCategoriesById(Id);
    
    await this.waitForCategory();

    this.Category = JSON.parse(this.Category);
    M.updateTextFields();
    this.inputTitle = this.Category.name;
    this.titleElement!.nativeElement.focus();
    this.inputDescription = this.Category.description;
    this.adjustTextareaSize();
    this.IdSelected = Id;
  }

  adjustTextareaSize() {
    const textarea = this.textareaRef!.nativeElement;
    textarea.style.height = 'auto';
    textarea.style.height = textarea.scrollHeight + 'px';
  }

  waitForCategory(): Promise<void> {
    return new Promise<void>((resolve) => {
      const checkInterval = setInterval(() => {
        this.Category = localStorage.getItem('Category');
        if (this.Category !== null) {
          clearInterval(checkInterval);
          resolve();
        }
      }, 100);
    });
  }

  onFileSelected(event: any) {
    this.file = event.target.files[0];
  }
  
  onFormSubmit(event: any) {
    const clickedButton = event.submitter as HTMLButtonElement;

    console.log(clickedButton.name);
    
    if (clickedButton.name === 'btn-send') {
      this.AddCategory();
    } else if (clickedButton.name === 'btn-update') {
      this.UpdateCategory();
    }


  }

  AddCategory(){
    if(this.formForSubmit.valid) {
      this.categoriesAIService.AddCategory(this.formForSubmit.controls["Title"].value, this.formForSubmit.controls["Description"].value, this.file);      
    }
  }

  UpdateCategory(){
    if(this.formForSubmit.valid) {
      this.categoriesAIService.UpdateCategory(this.formForSubmit.controls["Title"].value, this.formForSubmit.controls["Description"].value, this.file, this.IdSelected!.toString()); 
    }
  }

  deleteCategory(){
    this.categoriesAIService.deleteCategories(this.IdSelected!);
  }

}



