import { Component, OnInit, Input } from '@angular/core';
import { FormGroup, FormControl, FormBuilder, Validators, ValidatorFn, AbstractControl, ValidationErrors } from '@angular/forms';
import { ResourceService, ResourceType } from '../../../resource.service';
import { Discount, DiscountType } from 'src/app/models/Discount';
import { VisitInfo } from 'src/app/models/VisitInfo';
import { VisitInfoService } from 'src/app/services/visit-info.service';
import { ToastrService } from 'ngx-toastr';

@Component({
    selector: 'app-discount-form',
    templateUrl: './discount-form.component.html',
    styleUrls: ['./discount-form.component.scss']
})
export class DiscountFormComponent implements OnInit {
    public discountForm: FormGroup;
    public info: VisitInfo;
    public discountType = DiscountType;

    @Input() public discount: Discount = { type: DiscountType.ForChild, description: '', discountValueInPercentage: 0, groupSizeForDiscount: 0 };
    @Input() public isForAdd = false;

    public get discountTypeGroup() { return this.discountForm.get('discountTypeGroup') as FormGroup; }
    public get type() { return this.discountForm.get('discountTypeGroup').get('type') as FormControl; }
    public get description() { return this.discountForm.get('description') as FormControl; }
    public get discountValueInPercentage() { return this.discountForm.get('discountValueInPercentage') as FormControl; }
    public get groupSizeForDiscount() { return this.discountForm.get('discountTypeGroup').get('groupSizeForDiscount') as FormControl; }

    constructor(private formBuilder: FormBuilder, private resourceService: ResourceService, private infoService: VisitInfoService, private toast: ToastrService) { }

    ngOnInit() {
        this.infoService.getRecentInfo().subscribe(i => this.info = i);

        this.discountForm = this.formBuilder.group({
            discountTypeGroup: this.formBuilder.group({
                groupSizeForDiscount: [this.discount.groupSizeForDiscount],
                type: [this.discount.type, Validators.required]
            }, { validators: [this.haveGroupSize, Validators.required] }),
            description: [this.discount.description, [Validators.maxLength(256), Validators.required]],
            discountValueInPercentage: [this.discount.discountValueInPercentage, [Validators.min(0), Validators.max(100), Validators.required]],
        });
    }

    public haveGroupSize: ValidatorFn = (control: FormGroup): ValidationErrors | null => {
        const discountType = control.get('type').value;
        const groupSize = control.get('groupSizeForDiscount').value;

        if (discountType !== DiscountType[DiscountType.ForGroup] && groupSize > 0) {
            return { invalidType: 'group size > 0 allowed only ForGroup type' };
        }

        return null;
    }

    public edit() {
        const updatedDiscount: Discount = {
            id: this.discount.id,
            createdAt: this.discount.createdAt,
            updatedAt: this.discount.updatedAt,
            description: this.description.value,
            type: this.type.value,
            groupSizeForDiscount: this.groupSizeForDiscount.value > 0 ? this.groupSizeForDiscount.value : null,
            discountValueInPercentage: this.discountValueInPercentage.value
        };

        if (this.isDifferent(this.discount, updatedDiscount)) {
            this.discount = this.resourceService.edit(updatedDiscount);
            this.toast.info('Discount has been edited.');
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
        this.toast.info('New discount has been added.');
    }

    public testCRUD() {
        if (this.isForAdd) {
            this.add();
        } else {
            this.edit();
        }
    }
}
