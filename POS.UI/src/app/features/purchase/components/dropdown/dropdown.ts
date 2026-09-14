import {ChangeDetectionStrategy, Component, inject} from '@angular/core';
import {FormsModule} from '@angular/forms';
import {
    TuiDropdown,
    TuiFilterByInputPipe,
    TuiIcon,
    TuiInput,
} from '@taiga-ui/core';
import { TuiDataListWrapper, TuiTooltip} from '@taiga-ui/kit';

@Component({
    imports: [
        FormsModule,
        TuiDataListWrapper,
        TuiDropdown,
        TuiFilterByInputPipe,
        TuiIcon,
        TuiInput,
        TuiTooltip,
    ],
    selector:'app-dropdown',
    templateUrl: './dropdown.html',
    changeDetection: ChangeDetectionStrategy.OnPush,
})
export default class Dropdown {
    protected value = '';
    protected readonly items = ['Date','InvoiceNumber','SupplierName'];
}