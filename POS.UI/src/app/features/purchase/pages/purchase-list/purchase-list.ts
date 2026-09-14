import {ChangeDetectionStrategy, Component} from '@angular/core';
import DatePicker from "../../components/date-picker/date-picker";
import Dropdown from "../../components/dropdown/dropdown";
import { TuiButtonX, TuiInput, TuiTextfieldComponent } from "@taiga-ui/core";
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

@Component({
    imports: [
    DatePicker,
    Dropdown,
    TuiInput,
    TuiButtonX,
    FormsModule,
     ReactiveFormsModule,
],
    templateUrl: './purchase-list.html',
    styleUrl: './purchase-list.scss',
    changeDetection: ChangeDetectionStrategy.OnPush,
})
export default class PurchaseList {
    protected value = '';

    clear(){
        this.value = ''
    }
}
        
