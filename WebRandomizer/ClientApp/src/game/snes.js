export function snesToPc(mapping, addr) {
    if (mapping === 'exhirom') {
        const ex = addr < 0x800000 ? 0x400000 : 0;
        const pc = addr & 0x3FFFFF;
        return ex | pc;
    }
    if (mapping === 'lorom') {
        return ((addr & 0x7F0000) >>> 1) | (addr & 0x7FFF);
    }
    throw new Error('No known addressing mode supplied');
}
