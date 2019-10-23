import { BaseResponseDataType } from './BaseResponseDataType';

export interface Discount extends BaseResponseDataType {
    type: DiscountType;
    description: string;
    discountValueInPercentage: number;
    groupSizeForDiscount?: number | null;
}

export enum DiscountType {
    ForChild,
    ForPensioner,
    ForStudent,
    ForDisabled,
    ForGroup,
    ForFamily
}
