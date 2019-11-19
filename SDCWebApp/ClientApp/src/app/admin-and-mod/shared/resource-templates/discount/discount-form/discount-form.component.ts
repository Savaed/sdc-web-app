import { Component, OnInit, Input } from '@angular/core';
import { FormGroup, FormControl, FormBuilder, Validators, ValidatorFn, AbstractControl, ValidationErrors } from '@angular/forms';
import { ResourceService, ResourceType } from '../../../resource.service';
import { Discount, DiscountType } from 'src/app/models/Discount';

@Component({
    selector: 'app-discount-form',
    templateUrl: './discount-form.component.html',
    styleUrls: ['./discount-form.component.scss']
})
export class DiscountFormComponent implements OnInit {
    @Input() public discount: Discount = { type: DiscountType.ForChild, description: '', discountValueInPercentage: 0, groupSizeForDiscount: 0 };
    @Input() public isForAdd = false;

    private discountForm: FormGroup;
    private discountType = DiscountType;

    private get type() { return this.discountForm.get('discountTypeGroup').get('type') as FormControl; }
    private get description() { return this.discountForm.get('description') as FormControl; }
    private get discountValueInPercentage() { return this.discountForm.get('discountValueInPercentage') as FormControl; }
    private get groupSizeForDiscount() { return this.discountForm.get('discountTypeGroup').get('groupSizeForDiscount') as FormControl; }

    constructor(private formBuilder: FormBuilder, private resourceService: ResourceService) { }

    ngOnInit() {

        // TODO: Add validation for type and group size

        this.discountForm = this.formBuilder.group({
            discountTypeGroup: this.formBuilder.group({
                groupSizeForDiscount: [{ value: this.discount.groupSizeForDiscount, disabled: false}, Validators.max(30)],
                type: [this.discount.type, Validators.required]
            }),
            description: [this.discount.description, [Validators.maxLength(256), Validators.required]],
            discountValueInPercentage: [this.discount.discountValueInPercentage, [Validators.min(0), Validators.max(100), Validators.required]],
        });
    }

    private haveGroupSize: ValidatorFn = (control: FormGroup): ValidationErrors | null => {
        const discountType = control.get('type').value;
        const groupSize = control.get('groupSizeForDiscount').value;

        if (discountType !== DiscountType.ForGroup && groupSize >= 0) {
            console.log('group size invalid');
            return { invalidType: 'test' };
        }

        return null;
    }

    private edit() {
        console.log('update discount');

        const updatedDiscount: Discount = {
            id: this.discount.id,
            createdAt: this.discount.createdAt,
            updatedAt: this.discount.updatedAt,
            description: this.description.value,
            type: this.type.value,
            groupSizeForDiscount: this.groupSizeForDiscount.value,
            discountValueInPercentage: this.discountValueInPercentage.value
        };

        if (this.isDifferent(this.discount, updatedDiscount)) {
            this.discount = this.resourceService.edit(updatedDiscount);
        }
    }

    private isDifferent(oldDiscount: Discount, newDiscount: Discount): boolean {
        return oldDiscount.type !== newDiscount.type
            || oldDiscount.description !== newDiscount.description
            || oldDiscount.discountValueInPercentage !== newDiscount.discountValueInPercentage
            || oldDiscount.groupSizeForDiscount !== newDiscount.groupSizeForDiscount;
    }

    private add() {
        const newDiscount: Discount = {
            description: this.description.value,
            type: this.type.value,
            groupSizeForDiscount: this.groupSizeForDiscount.value,
            discountValueInPercentage: this.discountValueInPercentage.value
        };

        this.resourceService.add(newDiscount, ResourceType.Discount);
    }

    private testCRUD() {
        if (this.isForAdd) {
            this.add();
        } else {
            this.edit();
        }
    }

}
